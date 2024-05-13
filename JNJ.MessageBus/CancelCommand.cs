using MediatR;

namespace JNJ.MessageBus;

public record CancelCommand(Guid CancellationTargetId) : IRequest;