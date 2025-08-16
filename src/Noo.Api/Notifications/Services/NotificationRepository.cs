using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Notifications.Filters;
using Noo.Api.Notifications.Models;

namespace Noo.Api.Notifications.Services;

public class NotificationRepository : Repository<NotificationModel>, INotificationRepository
{
    public Task<SearchResult<NotificationModel>> GetForUserAsync(Ulid userId, NotificationFilter filter)
    {
        filter.UserId = userId;
        return SearchAsync(filter);
    }

    public async Task MarkAsReadAsync(Ulid userId, Ulid notificationId)
    {
        var set = Context.GetDbSet<NotificationModel>();
        await set
            .Where(x => x.Id == notificationId && x.UserId == userId)
            .ExecuteUpdateAsync(s => s.SetProperty(y => y.IsRead, true));
    }

    public async Task DeleteForUserAsync(Ulid userId, Ulid notificationId)
    {
        var set = Context.GetDbSet<NotificationModel>();
        await set
            .Where(x => x.Id == notificationId && x.UserId == userId)
            .ExecuteDeleteAsync();
    }
}

public static class IUnitOfWorkNotificationRepositoryExtensions
{
    public static INotificationRepository NotificationRepository(this IUnitOfWork unitOfWork)
    {
        return new NotificationRepository
        {
            Context = unitOfWork.Context
        };
    }
}

