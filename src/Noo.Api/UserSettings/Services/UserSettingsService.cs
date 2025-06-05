using Noo.Api.Core.Utils.DI;
using Noo.Api.UserSettings.DTO;

namespace Noo.Api.UserSettings.Services;

[RegisterScoped(typeof(IUserSettingsService))]
public class UserSettingsService : IUserSettingsService
{
    public Task<UserSettingsDTO> GetUserSettingsAsync(Ulid userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserSettingsAsync(Ulid userId, UserSettingsUpdateDTO userSettings)
    {
        throw new NotImplementedException();
    }
}
