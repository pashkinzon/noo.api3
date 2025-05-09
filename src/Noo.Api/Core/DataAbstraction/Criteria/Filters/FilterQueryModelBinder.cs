using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Noo.Api.Core.DataAbstraction.Criteria.Filters;

public class FilterQueryModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        var result = new Dictionary<string, string>();

        var query = bindingContext.HttpContext.Request.Query;

        if (query == null)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        foreach (var key in query.Keys.Where(k => k.StartsWith("filter[") && k.EndsWith(']')))
        {
            var propertyName = key[7..^1];
            result[propertyName] = query[key]!;
        }

        bindingContext.Result = ModelBindingResult.Success(result);
        return Task.CompletedTask;
    }
}
