using MediatR;
using Microsoft.Extensions.Logging;
using Users.Contracts;

namespace Users;

internal class AnotherUserSubmittedEventHandler : INotificationHandler<UserSubmittedEvent>
{
	private readonly ILogger<AnotherUserSubmittedEventHandler> _logger;

	public AnotherUserSubmittedEventHandler(ILogger<AnotherUserSubmittedEventHandler> logger)
    {
		_logger = logger;
	}

    public async Task Handle(UserSubmittedEvent notification, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Handling {eventId} {handlerName}'", notification.Id, nameof(AnotherUserSubmittedEventHandler));

		// Some delay to simulate processing
		await Task.Delay(20000, cancellationToken);

        _logger.LogInformation("Handled: {eventId} {handlerName}'", notification.Id, nameof(AnotherUserSubmittedEventHandler));

    }
}
