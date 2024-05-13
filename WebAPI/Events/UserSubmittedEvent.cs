using JNJ.MessageBus;

namespace WebAPI.Events;

internal record UserSubmittedEvent(User User) : Event;