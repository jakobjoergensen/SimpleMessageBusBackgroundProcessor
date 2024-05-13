using Microsoft.Extensions.Hosting;

namespace MessageBus;

internal interface IEventProcessor : IHostedService
{
    void CancelTask(Guid eventId);
}