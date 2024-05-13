using MediatR;
using MessageBus;
using Microsoft.Extensions.Logging;
using Users.Contracts;

namespace Users;

internal class UserSubmittedEventHandler : INotificationHandler<UserSubmittedEvent>
{
	private readonly ILogger<UserSubmittedEventHandler> _logger;
    private readonly IEventBus _eventBus;

    public UserSubmittedEventHandler(ILogger<UserSubmittedEventHandler> logger, IEventBus eventBus)
    {
		_logger = logger;
        _eventBus = eventBus;
    }

    public async Task Handle(UserSubmittedEvent notification, CancellationToken cancellationToken)
	{
        _logger.LogInformation("Handling {eventId} {handlerName}'", notification.Id, nameof(UserSubmittedEventHandler));

        // Some delay to simulate processing
        await Task.Delay(4000, cancellationToken);

        _logger.LogInformation("Handled: {eventId} {handlerName}'", notification.Id, nameof(UserSubmittedEventHandler));


        var username = "John Doe";
		var useremail = "john@example.com";

		await _eventBus.Publish(new UserRegisteredEvent(username, useremail));
	}
}
