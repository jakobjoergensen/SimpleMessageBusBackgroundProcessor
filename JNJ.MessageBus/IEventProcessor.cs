using Microsoft.Extensions.Hosting;

namespace JNJ.MessageBus;

internal interface IEventProcessor : IHostedService
{
    void CancelTask(Guid eventId);
}