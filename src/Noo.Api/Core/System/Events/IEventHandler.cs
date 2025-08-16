namespace Noo.Api.Core.System.Events;

public interface IEventHandler<in TEvent> where TEvent : IDomainEvent
{
    public Task HandleAsync(TEvent @event, CancellationToken ct = default);
}
