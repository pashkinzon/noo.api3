namespace Noo.Api.Core.System.Events;

public interface IEventPublisher
{
    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken ct = default) where TEvent : IDomainEvent;
}
