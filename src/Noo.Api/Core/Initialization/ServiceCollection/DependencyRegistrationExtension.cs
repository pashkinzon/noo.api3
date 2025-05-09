using System.Reflection;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class DependencyRegistrationExtension
{
    public static void RegisterDependencies(this IServiceCollection services)
    {
        RegisterServices(services);
    }

    private static void RegisterServices(IServiceCollection services)
    {
        var types = GetClassesImplementing<RegisterClassAttribute>();

        foreach (var type in types)
        {
            var attribute = type.GetCustomAttribute<RegisterClassAttribute>()!;
            var serviceType = attribute.Type ?? type;

            switch (attribute.Scope)
            {
                case ClassRegistrationScope.Transient:
                    services.AddTransient(serviceType, type);
                    break;
                case ClassRegistrationScope.Scoped:
                    services.AddScoped(serviceType, type);
                    break;
                case ClassRegistrationScope.Singleton:
                    services.AddSingleton(serviceType, type);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unknown registration scope");
            }
        }
    }

    private static Type[] GetClassesImplementing<TAttribute>() where TAttribute : Attribute
    {
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttribute<TAttribute>() != null)
            .ToArray();
    }
}
