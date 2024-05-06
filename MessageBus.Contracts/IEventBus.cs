namespace MessageBus.Contracts;

public interface IEventBus
{
	Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class, IEvent;
}
