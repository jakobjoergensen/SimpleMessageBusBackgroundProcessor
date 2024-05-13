using MediatR;
using MessageBus;
using Microsoft.Extensions.Logging;

namespace Users;

internal class UserRegisteredConsumer : INotificationHandler<UserRegisteredEvent>
{
    private readonly ILogger<UserRegisteredConsumer> _logger;
    private readonly IEventBus _eventBus;

    public UserRegisteredConsumer(ILogger<UserRegisteredConsumer> logger, IEventBus eventBus)
    {
        _logger = logger;
        _eventBus = eventBus;
    }


    public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending confirmation email to '{username}' at '{useremail}' ...", notification.UserName, notification.UserEmail);

        await Task.Delay(2500);

        _logger.LogInformation("Sent!");

        await _eventBus.Publish(new ConfirmationEmailSentEvent());
    }
}
