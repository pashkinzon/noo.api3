using Noo.Api.Core.Exceptions;
using Noo.Api.Core.Utils.Json;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class ControllersAndJsonExtension
{
    public static void AddNooControllersAndConfigureJson(this IServiceCollection services)
    {
        var exceptionHandler = services.BuildServiceProvider().GetService<PipelineExceptionHandler>()
            ?? throw new InvalidOperationException("PipelineExceptionHandler is not registered.");

        services
            .AddControllers()
            .AddJsonOptions(
                options => options.JsonSerializerOptions.Converters.Add(new HyphenLowerCaseStringEnumConverterFactory())
            )
            .ConfigureApiBehaviorOptions(
                options => options.InvalidModelStateResponseFactory = exceptionHandler.HandleBadRequestException
            );
    }
}
