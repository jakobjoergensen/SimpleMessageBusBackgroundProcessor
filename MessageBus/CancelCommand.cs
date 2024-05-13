using MediatR;

namespace MessageBus;

public record CancelCommand(Guid CancellationTargetId) : IRequest;