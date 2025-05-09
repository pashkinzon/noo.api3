using System.Collections.Concurrent;
using System.Reflection;

namespace Noo.Api.Core.DataAbstraction.Model;

public abstract class ModelAttributeResolver<TModel> where TModel : BaseModel
{
    private static ConcurrentDictionary<string, Attribute[]> Сache { get; } = [];

    protected static TAttribute? GetAttribute<TAttribute>(string propertyName) where TAttribute : Attribute
    {
        if (IsAttributeCached<TAttribute>(propertyName, out var attributes))
        {
            return attributes.OfType<TAttribute>().FirstOrDefault();
        }

        var property = GetProperty(propertyName);

        if (property == null)
        {
            return null;
        }

        var attribute = property.GetCustomAttribute<TAttribute>();

        if (attribute == null)
        {
            return null;
        }

        SetCachedAttribute<TAttribute>(property, attribute);

        return attribute;
    }

    protected static PropertyInfo? GetProperty(string propertyName)
    {
        var parts = propertyName.Split('.', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 1)
        {
            return typeof(TModel).GetProperty(propertyName);
        }

        var property = typeof(TModel).GetProperty(parts[0]);

        if (property == null)
        {
            return null;
        }

        return GetProperty(string.Join(".", parts.Skip(1)));
    }

    private static bool IsAttributeCached<TAttribute>(
        string propertyName,
        out Attribute[] attributes
    ) where TAttribute : Attribute
    {
        var key = getKey(propertyName);

        if (Сache.TryGetValue(key, out var cachedAttributes))
        {
            attributes = cachedAttributes;
            return true;
        }

        attributes = [];
        return false;
    }

    private static void SetCachedAttribute<TAttribute>(
        PropertyInfo property,
        Attribute attribute
    ) where TAttribute : Attribute
    {
        var key = getKey(property.Name);

        Сache.AddOrUpdate(key, [attribute], (_, attributes) =>
        {
            if (attributes.OfType<TAttribute>().FirstOrDefault() == null)
            {
                attributes.Append(attribute);
            }

            return attributes;
        });
    }

    private static string getKey(string propertyName)
    {
        return $"{typeof(TModel).FullName}.{propertyName}";
    }
}
