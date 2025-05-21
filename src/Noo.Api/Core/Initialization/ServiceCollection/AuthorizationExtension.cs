using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class AuthorizationExtension
{
    public static void AddNooAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            foreach (var registrar in GetPolicyRegistrars())
            {
                registrar.RegisterPolicies(options);
            }
        });
    }

    private static IEnumerable<IPolicyRegistrar> GetPolicyRegistrars()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(
                type => typeof(IPolicyRegistrar).IsAssignableFrom(type) &&
                !type.IsInterface &&
                !type.IsAbstract
            )
            .Select(type => (IPolicyRegistrar)Activator.CreateInstance(type)!);
    }
}
