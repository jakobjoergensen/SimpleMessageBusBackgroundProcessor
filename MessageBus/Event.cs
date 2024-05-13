﻿using MediatR;

namespace MessageBus;

public record Event : INotification
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid CancellationId { get; init; } = Guid.NewGuid();
}
