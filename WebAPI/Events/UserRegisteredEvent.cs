using MessageBus;

namespace WebAPI.Events;

internal record UserRegisteredEvent(User User) : Event;