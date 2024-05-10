
namespace MessageBus;

public record CancellationEvent(Guid TargetEventId) : IEvent
{
    public Guid Id { get; init; } = Guid.NewGuid();
}
