using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.Config.Env;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Initialization.Configuration;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class DbContextExtension
{
    public static void AddNooDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConfig = configuration.GetSection(DbConfig.SectionName).GetOrThrow<DbConfig>();

        services.AddDbContext<NooDbContext>(options =>
        {
            options.UseMySql(
                dbConfig.ConnectionString,
                ServerVersion.AutoDetect(dbConfig.ConnectionString),
                builder => builder.CommandTimeout(dbConfig.CommandTimeout).EnableIndexOptimizedBooleanColumns()
            );
        });
    }
}
