using MessageBus;

namespace WebAPI.Events;

internal record ConfirmationEmailSentEvent() : Event;