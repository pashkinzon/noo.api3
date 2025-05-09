namespace Noo.Api.Core.Utils.DI;

[AttributeUsage(AttributeTargets.Class)]
public abstract class RegisterClassAttribute : Attribute
{
    public Type? Type { get; init; }

    public ClassRegistrationScope Scope { get; init; }

    public RegisterClassAttribute(ClassRegistrationScope scope, Type? type = null)
    {
        Scope = scope;
        Type = type;
    }
}
