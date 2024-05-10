using Microsoft.Extensions.Hosting;

namespace MessageBus;

public interface IEventProcessor : IHostedService
{
    void CancelTask(Guid eventId);
}