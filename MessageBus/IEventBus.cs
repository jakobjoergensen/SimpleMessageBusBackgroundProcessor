namespace MessageBus;

public interface IEventBus
{
    Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : Event;
}
