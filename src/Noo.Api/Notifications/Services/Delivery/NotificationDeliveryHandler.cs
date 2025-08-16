using Noo.Api.Core.System.Events;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Notifications.Services;
using Noo.Api.Notifications.Services.Delivery;

namespace Noo.Api.Notifications;

[RegisterScoped(typeof(IEventHandler<NotificationCreatedEvent>))]
public class NotificationDeliveryHandler : IEventHandler<NotificationCreatedEvent>
{
    private readonly IEnumerable<INotificationChannel> _channels;

    public NotificationDeliveryHandler(IEnumerable<INotificationChannel> channels)
    {
        _channels = channels;
    }

    public async Task HandleAsync(NotificationCreatedEvent @event, CancellationToken ct = default)
    {
        var allowed = @event.Channels?.ToHashSet();
        var selected = allowed == null
            ? _channels
            : _channels.Where(c => allowed.Contains(c.Channel));

        foreach (var channel in selected)
        {
            await channel.SendAsync(@event.Model, ct);
        }
    }
}
