using MessageBus;

namespace Users;

internal record UserRegisteredEvent(string UserName, string UserEmail) : Event;