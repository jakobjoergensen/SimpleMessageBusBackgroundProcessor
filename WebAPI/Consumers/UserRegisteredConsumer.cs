using JNJ.MessageBus;
using MediatR;
using WebAPI.Events;

namespace WebAPI.Consumers;

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
        try
        {
            _logger.LogInformation("Sending confirmation email to '{username}' at '{useremail}' ...", notification.User.UserName, notification.User.Email);

            await Task.Delay(2500);

            _logger.LogInformation("Sent!");

            var confirmationEmailSentEvent = new ConfirmationEmailSentEvent();
            await _eventBus.Publish(confirmationEmailSentEvent);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("CANCELED: Sending confirmation email to '{username}' at '{useremail}' ...", notification.User.UserName, notification.User?.Email);
            throw;
        }
    }
}
