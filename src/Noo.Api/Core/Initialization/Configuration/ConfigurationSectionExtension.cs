using Noo.Api.Core.Config;

namespace Noo.Api.Core.Initialization.Configuration;

public static class ConfigurationSectionExtension
{
    public static T GetOrThrow<T>(this IConfiguration configuration) where T : IConfig
    {
        var section = configuration.Get<T>();

        if (section == null)
        {
            throw new MissingConfigException<T>();
        }

        return section;
    }
}
