using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.UserSettings.Models;

namespace Noo.Api.UserSettings.Services;

public class UserSettingsRepository : Repository<UserSettingsModel>, IUserSettingsRepository
{
    public async Task<UserSettingsModel> GetOrCreateAsync(Ulid userId)
    {
        var settings = await Context.GetDbSet<UserSettingsModel>().FirstOrDefaultAsync(settings => settings.UserId == userId);

        if (settings is null)
        {
            settings = new UserSettingsModel
            {
                UserId = userId
            };

            Context.GetDbSet<UserSettingsModel>().Add(settings);
        }

        return settings;
    }
}

public static class IUnitOfWorkUserSettingsRepositoryExtensions
{
    public static IUserSettingsRepository UserSettingsRepository(this IUnitOfWork unitOfWork)
    {
        return new UserSettingsRepository()
        {
            Context = unitOfWork.Context
        };
    }
}
