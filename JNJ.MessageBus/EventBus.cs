namespace JNJ.MessageBus;

internal sealed class EventBus : IEventBus
{
    private readonly InMemoryMessageQueue _queue;

    public EventBus(InMemoryMessageQueue queue)
    {
        _queue = queue;
    }

    public async Task Publish<T>(T message, CancellationToken cancellationToken) where T : Event
    {
        await _queue.Writer.WriteAsync(message, cancellationToken);
    }
}
