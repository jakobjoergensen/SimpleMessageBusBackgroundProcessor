using MediatR;
using MessageBus;
using WebAPI.Events;

namespace WebAPI.Consumers;

internal class UserSubmittedConsumer : INotificationHandler<UserSubmittedEvent>
{
    private readonly ILogger<UserSubmittedConsumer> _logger;
    private readonly IEventBus _eventBus;

    public UserSubmittedConsumer(ILogger<UserSubmittedConsumer> logger, IEventBus eventBus)
    {
        _logger = logger;
        _eventBus = eventBus;
    }

    public async Task Handle(UserSubmittedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handling {eventId} {handlerName}'. UserName: {username}, Email: {email}", notification.Id, nameof(UserSubmittedConsumer), notification.User.UserName, notification.User.Email);

            // Some delay to simulate processing
            await Task.Delay(10000, cancellationToken);

            _logger.LogInformation("Handled: {eventId} {handlerName}'", notification.Id, nameof(UserSubmittedConsumer));

            await _eventBus.Publish(new UserRegisteredEvent(notification.User));
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("CANCELED: Handling {eventId} {handlerName}'", notification.Id, nameof(UserSubmittedConsumer));
            throw;
        }

    }
}
