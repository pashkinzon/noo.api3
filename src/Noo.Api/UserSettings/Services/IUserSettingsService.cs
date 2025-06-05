
using Noo.Api.UserSettings.DTO;

namespace Noo.Api.UserSettings.Services;

public interface IUserSettingsService
{
    public Task<UserSettingsDTO> GetUserSettingsAsync(Ulid userId);
    public Task UpdateUserSettingsAsync(Ulid userId, UserSettingsUpdateDTO userSettings);
}
