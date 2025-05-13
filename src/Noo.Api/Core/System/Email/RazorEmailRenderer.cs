
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.Core.System.Email;

[RegisterTransient(typeof(IEmailRenderer))]
public class EmailRenderer : IEmailRenderer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILoggerFactory _loggerFactory;

    public EmailRenderer(IServiceProvider provider, ILoggerFactory loggerFactory)
    {
        _serviceProvider = provider;
        _loggerFactory = loggerFactory;
    }

    public async Task<string> RenderEmailAsync<TComponent, TParams>(TParams parameters) where TComponent : IComponent where TParams : class
    {
        await using var htmlRenderer = new HtmlRenderer(_serviceProvider, _loggerFactory);

        return await htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var parameterView = ConvertToParameterView(parameters);
            var output = await htmlRenderer.RenderComponentAsync<TComponent>(parameterView);

            return output.ToHtmlString();
        });
    }

    private static ParameterView ConvertToParameterView<TParams>(TParams parameters) where TParams : class
    {
        var parameterDictionary = new Dictionary<string, object?>();

        foreach (var property in typeof(TParams).GetProperties())
        {
            var value = property.GetValue(parameters);
            parameterDictionary.Add(property.Name, value);
        }

        return ParameterView.FromDictionary(parameterDictionary);
    }
}
