using MediatR;
using MessageBus;
using Microsoft.Extensions.Logging;
using Users.Contracts;

namespace Users;

internal class UserSubmittedEventConsumer : INotificationHandler<UserSubmittedEvent>
{
	private readonly ILogger<UserSubmittedEventConsumer> _logger;
    private readonly IEventBus _eventBus;

    public UserSubmittedEventConsumer(ILogger<UserSubmittedEventConsumer> logger, IEventBus eventBus)
    {
		_logger = logger;
        _eventBus = eventBus;
    }

    public async Task Handle(UserSubmittedEvent notification, CancellationToken cancellationToken)
	{
        _logger.LogInformation("Handling {eventId} {handlerName}'", notification.Id, nameof(UserSubmittedEventConsumer));

        // Some delay to simulate processing
        await Task.Delay(4000, cancellationToken);

        _logger.LogInformation("Handled: {eventId} {handlerName}'", notification.Id, nameof(UserSubmittedEventConsumer));


        var username = "John Doe";
		var useremail = "john@example.com";

		await _eventBus.Publish(new UserRegisteredEvent(username, useremail));
	}
}
