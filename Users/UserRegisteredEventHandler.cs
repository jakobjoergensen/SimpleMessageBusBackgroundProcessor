using MediatR;
using Microsoft.Extensions.Logging;
using Users.Contracts;

namespace Users;

internal class UserRegisteredEventHandler : INotificationHandler<UserRegisteredEvent>
{
	private readonly ILogger<UserRegisteredEventHandler> _logger;

	public UserRegisteredEventHandler(ILogger<UserRegisteredEventHandler> logger)
    {
		_logger = logger;
	}

    public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
	{
		_logger.LogInformation("{eventId}: UserName '{username}', email '{email}'", notification.Id, notification.UserName, notification.UserEmail);

		// Some random delay time to simulate processing
		var random = new Random();
		var delay = random.Next(1000, 5000);
		await Task.Delay(delay);
	}
}
