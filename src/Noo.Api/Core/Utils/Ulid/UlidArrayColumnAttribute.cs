namespace Noo.Api.Core.Utils.Ulid;

[AttributeUsage(AttributeTargets.Property)]
public class UlidArrayColumnAttribute : Attribute
{
    public string Name { get; set; }

    public UlidArrayColumnAttribute(string name)
    {
        Name = name;
    }
}
