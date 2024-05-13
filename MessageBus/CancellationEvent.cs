namespace MessageBus;

public record CancellationEvent(Guid CancellationTargetId) : Event;