namespace Noo.Api.Core.Utils.Json;

[AttributeUsage(AttributeTargets.Property)]
public class JsonColumnAttribute : Attribute
{
    public string Name { get; set; }

    public Type? Converter { get; set; }

    public JsonColumnAttribute(string name)
    {
        Name = name;
    }
}
