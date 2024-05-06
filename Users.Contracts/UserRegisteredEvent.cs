using MessageBus.Contracts;

namespace Users.Contracts;

public class UserRegisteredEvent : IEvent
{
    public Guid Id { get; init;  } = Guid.NewGuid();
    public string UserName { get; init; }
    public string UserEmail { get; init; }

    public UserRegisteredEvent(string userName, string userEmail)
    {
		UserName = userName;
		UserEmail = userEmail;
	}
}