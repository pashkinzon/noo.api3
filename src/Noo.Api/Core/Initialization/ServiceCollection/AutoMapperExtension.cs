using AutoMapper;
using Noo.Api.Core.Utils.AutoMapper;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class AutoMapperExtension
{
    public static void AddAutoMapperProfiles(this IServiceCollection services)
    {
        var profiles = _getProfiles();

        var config = new MapperConfiguration(config =>
        {
            foreach (var profile in profiles)
            {
                config.AddProfile(profile);
            }
        });

        //config.AssertConfigurationIsValid();

        services.AddSingleton(config);
        services.AddSingleton(_ => config.CreateMapper());
    }

    private static IEnumerable<Type> _getProfiles()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        return assemblies.SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttributes(typeof(AutoMapperProfileAttribute), false).Length != 0)
            .ToList();
    }
}
