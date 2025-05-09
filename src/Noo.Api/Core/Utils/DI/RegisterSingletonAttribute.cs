namespace Noo.Api.Core.Utils.DI;

[AttributeUsage(AttributeTargets.Class)]
public class RegisterSingletonAttribute : RegisterClassAttribute
{
    public RegisterSingletonAttribute(Type? type = null) : base(ClassRegistrationScope.Singleton, type) { }
}
