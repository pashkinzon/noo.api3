using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.UserSettings.Models;

namespace Noo.Api.UserSettings.Services;

public interface IUserSettingsRepository : IRepository<UserSettingsModel>
{
    public Task<UserSettingsModel> GetOrCreateAsync(Ulid userId);
}
