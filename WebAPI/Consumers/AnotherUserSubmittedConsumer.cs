using MediatR;
using WebAPI.Events;

namespace WebAPI.Consumers;

internal class AnotherUserSubmittedConsumer : INotificationHandler<UserSubmittedEvent>
{
    private readonly ILogger<AnotherUserSubmittedConsumer> _logger;

    public AnotherUserSubmittedConsumer(ILogger<AnotherUserSubmittedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Handle(UserSubmittedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handling {eventId} {handlerName}'. UserName: {username}, Email: {email}", notification.Id, nameof(AnotherUserSubmittedConsumer), notification.User.UserName, notification.User.Email);

            // Some delay to simulate processing
            await Task.Delay(20000, cancellationToken);

            _logger.LogInformation("Handled: {eventId} {handlerName}'", notification.Id, nameof(AnotherUserSubmittedConsumer));
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("CANCELED: {eventId} {handlerName} (cancellationId: {cancellationId}", notification.Id, nameof(AnotherUserSubmittedConsumer), notification.CancellationId);
            throw;
        }

    }
}
