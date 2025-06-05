using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Notifications.DTO;
using Noo.Api.Notifications.Models;

namespace Noo.Api.Notifications.Services;

[RegisterScoped(typeof(INotificationService))]
public class NotificationService : INotificationService
{
    public Task BulkCreateNotificationsAsync(BulkCreateNotificationsDTO options)
    {
        throw new NotImplementedException();
    }

    public Task DeleteNotificationAsync(Ulid notificationId, Ulid userId)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<NotificationDTO>, int)> GetNotificationsAsync(Ulid userId, Criteria<NotificationModel> criteria)
    {
        throw new NotImplementedException();
    }

    public Task MarkAsReadAsync(Ulid userId, Ulid notificationId)
    {
        throw new NotImplementedException();
    }
}
