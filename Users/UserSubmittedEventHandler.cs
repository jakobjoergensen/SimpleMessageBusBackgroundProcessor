using MediatR;
using Microsoft.Extensions.Logging;
using Users.Contracts;

namespace Users;

internal class UserSubmittedEventHandler : INotificationHandler<UserSubmittedEvent>
{
	private readonly ILogger<UserSubmittedEventHandler> _logger;

	public UserSubmittedEventHandler(ILogger<UserSubmittedEventHandler> logger)
    {
		_logger = logger;
	}

    public async Task Handle(UserSubmittedEvent notification, CancellationToken cancellationToken)
	{
		_logger.LogInformation("{eventId}: Handling'", notification.Id);

		// Some delay to simulate processing
		await Task.Delay(30000, cancellationToken);
	}
}
