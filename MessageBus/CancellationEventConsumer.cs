using MediatR;

namespace MessageBus;

internal class CancellationEventConsumer : INotificationHandler<CancellationEvent>
{
    private readonly IEventProcessor _eventProcessor;

    public CancellationEventConsumer(IEventProcessor eventProcessor)
    {
        _eventProcessor = eventProcessor;
    }

    public Task Handle(CancellationEvent notification, CancellationToken cancellationToken)
    {
        _eventProcessor.CancelTask(notification.CancellationTargetId);
        return Task.CompletedTask;
    }
}
