
using Noo.Api.UserSettings.DTO;
using Noo.Api.UserSettings.Models;

namespace Noo.Api.UserSettings.Services;

public interface IUserSettingsService
{
    public Task<UserSettingsModel> GetUserSettingsAsync(Ulid userId);
    public Task UpdateUserSettingsAsync(Ulid userId, UserSettingsUpdateDTO userSettings);
}
