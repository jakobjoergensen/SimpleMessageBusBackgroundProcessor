using MediatR;

namespace MessageBus;

internal class CancelCommandHandler : IRequestHandler<CancelCommand>
{
    private readonly IEventProcessor _eventProcessor;

    public CancelCommandHandler(IEventProcessor eventProcessor)
    {
        _eventProcessor = eventProcessor;
    }

    public Task Handle(CancelCommand notification, CancellationToken cancellationToken)
    {
        try
        {
            _eventProcessor.CancelTask(notification.CancellationTargetId);
            return Task.CompletedTask;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
