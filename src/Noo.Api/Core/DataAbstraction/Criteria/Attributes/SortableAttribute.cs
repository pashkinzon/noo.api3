namespace Noo.Api.Core.DataAbstraction.Criteria.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class SortableAttribute : Attribute
{
    public SortableAttribute()
    { }
}
