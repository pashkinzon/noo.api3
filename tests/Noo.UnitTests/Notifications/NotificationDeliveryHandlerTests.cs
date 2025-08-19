using Moq;
using Noo.Api.Notifications;
using Noo.Api.Notifications.Services;
using Noo.Api.Notifications.Models;
using Noo.Api.Notifications.Services.Delivery;
using Noo.Api.Notifications.Types;

namespace Noo.UnitTests.Notifications;

public class NotificationDeliveryHandlerTests
{
    private sealed class TestChannel(string name) : INotificationChannel
    {
        public NotificationChannelType Channel => Enum.Parse<NotificationChannelType>(name);
        public Task SendAsync(NotificationModel model, CancellationToken ct = default) => Task.CompletedTask;
    }

    [Fact]
    public async Task Sends_To_All_When_No_Filter()
    {
        var model = new NotificationModel { Title = "t" };
        var ch1 = new Mock<INotificationChannel>();
        ch1.SetupGet(c => c.Channel).Returns(NotificationChannelType.Http);
        ch1.Setup(c => c.SendAsync(model, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();
        var ch2 = new Mock<INotificationChannel>();
        ch2.SetupGet(c => c.Channel).Returns(NotificationChannelType.Telegram);
        ch2.Setup(c => c.SendAsync(model, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();

        var handler = new NotificationDeliveryHandler(new[] { ch1.Object, ch2.Object });
        await handler.HandleAsync(new NotificationCreatedEvent(model, null));

        ch1.Verify();
        ch2.Verify();
    }

    [Fact]
    public async Task Sends_Only_Selected_Channels()
    {
        var model = new NotificationModel { Title = "t" };
        var ch1 = new Mock<INotificationChannel>();
        ch1.SetupGet(c => c.Channel).Returns(NotificationChannelType.Http);
        ch1.Setup(c => c.SendAsync(model, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();
        var ch2 = new Mock<INotificationChannel>();
        ch2.SetupGet(c => c.Channel).Returns(NotificationChannelType.Telegram);

        var handler = new NotificationDeliveryHandler(new[] { ch1.Object, ch2.Object });
        await handler.HandleAsync(new NotificationCreatedEvent(model, new[] { NotificationChannelType.Http }));

        ch1.Verify();
        ch2.Verify(c => c.SendAsync(It.IsAny<NotificationModel>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
