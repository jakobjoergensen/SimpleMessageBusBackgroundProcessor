using MessageBus;

namespace Users.Contracts;

public class UserSubmittedEvent : IEvent
{
    public Guid Id { get; init;  } = Guid.NewGuid();
}