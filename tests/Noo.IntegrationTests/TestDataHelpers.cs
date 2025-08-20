using Microsoft.Extensions.DependencyInjection;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Sessions.Models;
using Noo.Api.Users.Models;
using Noo.Api.Core.Utils.UserAgent;

namespace Noo.IntegrationTests;

public static class TestDataHelpers
{
    public static Ulid GetUserId(ApiFactory factory, string username)
    {
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<NooDbContext>();
        var users = db.GetDbSet<UserModel>();
        var user = users.First(u => u.Username == username);
        return user.Id;
    }

    public static async Task<Ulid> CreateSessionAsync(ApiFactory factory, Ulid userId, string? deviceId = null, string? userAgent = null)
    {
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<NooDbContext>();
        var set = db.GetDbSet<SessionModel>();

        var model = new SessionModel
        {
            UserId = userId,
            DeviceId = deviceId,
            UserAgent = userAgent ?? "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36",
            Browser = "Chrome",
            Os = "Linux",
            DeviceType = DeviceType.Desktop,
            LastRequestAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        set.Add(model);
        await db.SaveChangesAsync();
        return model.Id;
    }

    public static bool SessionExists(ApiFactory factory, Ulid sessionId)
    {
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<NooDbContext>();
        var set = db.GetDbSet<SessionModel>();
        return set.Any(s => s.Id == sessionId);
    }
}
