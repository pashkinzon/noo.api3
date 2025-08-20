using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Noo.Api.Core.Config.Env;
using Noo.Api.Core.DataAbstraction.Cache;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Sessions.Services;

namespace Noo.UnitTests.Sessions;

public class OnlineServiceTests
{
    private static NooDbContext CreateDb()
    {
        var options = new DbContextOptionsBuilder<NooDbContext>()
            .UseInMemoryDatabase($"db-{Guid.NewGuid()}")
            .Options;

        var services = new ServiceCollection();
        services.AddOptions<DbConfig>();

        var sp = services.BuildServiceProvider();
        var cfg = sp.GetRequiredService<IOptions<DbConfig>>();
        return new NooDbContext(cfg, options);
    }

    private static ICacheRepository CreateCache() => new MemoryCacheRepository();

    [Fact(DisplayName = "OnlineService: setting user online updates last-seen and counts")]
    public async Task SetUserOnline_and_GetLastOnline_works()
    {
        var db = CreateDb();
        var cache = CreateCache();
        var service = new OnlineService(cache, db);
        var userId = Ulid.NewUlid();

        await service.SetUserOnlineAsync(userId);
        var last = await service.GetLastOnlineByUserAsync(userId);

        Assert.True(last.HasValue);
        Assert.True(DateTime.UtcNow - last.Value < TimeSpan.FromMinutes(1));
        Assert.True(await service.IsUserOnlineAsync(userId));
        var count = await service.GetOnlineCountAsync();
        Assert.True(count >= 1);
    }

    [Fact(DisplayName = "OnlineService: setting session online updates last-seen by session")]
    public async Task SetSessionOnline_and_fetch_by_session_works()
    {
        var db = CreateDb();
        var cache = CreateCache();
        var service = new OnlineService(cache, db);
        var sessionId = Ulid.NewUlid();

        await service.SetSessionOnlineAsync(sessionId);
        var last = await service.GetLastOnlineBySessionAsync(sessionId);

        Assert.True(last.HasValue);
        Assert.True(DateTime.UtcNow - last.Value < TimeSpan.FromMinutes(1));
    }
}

