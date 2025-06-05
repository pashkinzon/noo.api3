using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Notifications.DTO;
using Noo.Api.Notifications.Models;

namespace Noo.Api.Notifications.Services;

public interface INotificationService
{
    public Task BulkCreateNotificationsAsync(BulkCreateNotificationsDTO options);
    public Task DeleteNotificationAsync(Ulid notificationId, Ulid userId);
    public Task<(IEnumerable<NotificationDTO>, int)> GetNotificationsAsync(Ulid userId, Criteria<NotificationModel> criteria);
    public Task MarkAsReadAsync(Ulid userId, Ulid notificationId);
}
