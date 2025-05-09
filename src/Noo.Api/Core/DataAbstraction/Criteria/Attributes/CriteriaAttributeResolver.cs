using Noo.Api.Core.DataAbstraction.Criteria.Filters;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.Core.DataAbstraction.Criteria.Attributes;

public class CriteriaAttributeResolver<TModel> : ModelAttributeResolver<TModel> where TModel : BaseModel
{
    public static bool IsFilterable(string propertyName, FilterType filterType)
    {
        var filterableAttribute = GetAttribute<FilterableAttribute>(propertyName);

        if (filterableAttribute == null)
        {
            return false;
        }

        return filterableAttribute.AllowedFilterTypes.Contains(filterType);
    }

    public static bool IsSortable(string propertyName)
    {
        return GetAttribute<SortableAttribute>(propertyName) != null;
    }
}
