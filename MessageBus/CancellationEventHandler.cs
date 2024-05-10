using MediatR;

namespace MessageBus;

internal class CancellationEventHandler : INotificationHandler<CancellationEvent>
{
    private readonly IEventProcessor _eventProcessor;

    public CancellationEventHandler(IEventProcessor eventProcessor)
    {
        _eventProcessor = eventProcessor;
    }

    public Task Handle(CancellationEvent notification, CancellationToken cancellationToken)
    {
        _eventProcessor.CancelTask(notification.ToBeCanceledEventId);
        return Task.CompletedTask;
    }
}
