using MediatR;

namespace MessageBus.Contracts;

public interface IEvent : INotification
{
	Guid Id { get; init; }
}
