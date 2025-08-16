using Noo.Api.Notifications.Models;
using Noo.Api.Notifications.Types;

namespace Noo.Api.Notifications.Services.Delivery;

public interface INotificationChannel
{
    public NotificationChannelType Channel { get; }
    public Task SendAsync(NotificationModel model, CancellationToken ct = default);
}
