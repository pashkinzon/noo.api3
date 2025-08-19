using Microsoft.Extensions.Options;
using Noo.Api.Core.Config.Env;
using Noo.Api.Core.System.Events;

namespace Noo.UnitTests.Core.Events;

public class DomainEventQueueTests
{
    private sealed class DummyEvent : IDomainEvent { }

    [Fact]
    public void DropOnFull_Works_As_Configured()
    {
        var opts = Options.Create(new EventsConfig { QueueCapacity = 2 });
        var queue = new DomainEventQueue(opts);

        // Fill to capacity
        var ok1 = queue.TryEnqueue(new DummyEvent());
        var ok2 = queue.TryEnqueue(new DummyEvent());
        // With DropWrite, TryEnqueue returns true but the item is dropped when full
        var ok3 = queue.TryEnqueue(new DummyEvent());

        Assert.True(ok1);
        Assert.True(ok2);
        Assert.True(ok3);

        // Only two items should be readable
        var read = 0;
        while (queue.Reader.TryRead(out _)) read++;
        Assert.Equal(2, read);
    }
}
