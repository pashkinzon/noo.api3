using System.Threading.Channels;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.Core.System.Events;

[RegisterSingleton]
public class DomainEventQueue
{
    private static readonly int _defaultCapacity = 2048;
    private readonly Channel<IDomainEvent> _channel;

    public DomainEventQueue(Microsoft.Extensions.Options.IOptions<Noo.Api.Core.Config.Env.EventsConfig> options)
    {
        var configured = options?.Value?.QueueCapacity ?? _defaultCapacity;
        var capacity = Math.Clamp(configured, 1, 1_048_576);

        _channel = Channel.CreateBounded<IDomainEvent>(
            new BoundedChannelOptions(capacity)
            {
                SingleReader = true,
                SingleWriter = false,
                FullMode = BoundedChannelFullMode.DropWrite,
                AllowSynchronousContinuations = false
            }
        );
    }

    public bool TryEnqueue(IDomainEvent @event)
    {
        return _channel.Writer.TryWrite(@event);
    }

    public ChannelReader<IDomainEvent> Reader => _channel.Reader;
}
