using MessageBus;

namespace Users.Events;

internal record UserRegisteredEvent(string UserName, string UserEmail) : Event;