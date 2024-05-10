using MediatR;

namespace MessageBus;

public interface IEvent : INotification
{
    Guid Id { get; init; }
}
