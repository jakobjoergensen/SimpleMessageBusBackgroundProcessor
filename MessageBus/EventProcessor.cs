using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MessageBus;

internal class EventProcessor : BackgroundService
{
	private readonly InMemoryMessageQueue _queue;
	private readonly IServiceScopeFactory _serviceScopeFactory;
	private readonly ILogger<EventProcessor> _logger;

	public EventProcessor(InMemoryMessageQueue queue, IServiceScopeFactory serviceScopeFactory, ILogger<EventProcessor> logger)
    {
		_queue = queue;
		_serviceScopeFactory = serviceScopeFactory;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		await foreach (IEvent @event in _queue.Reader.ReadAllAsync(stoppingToken))
		{
			var watch = Stopwatch.StartNew();

			try
			{
				_logger.LogInformation("{eventId} {eventType} processing ...", @event.Id, @event.GetType());

				using (var scope = _serviceScopeFactory.CreateScope())
				{
					var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
					await publisher.Publish(@event, stoppingToken);
				}
			}
			catch (Exception ex) 
			{
				_logger.LogError(ex, "Something went wrong. Event {eventId}", @event.Id);
			}
			finally 
			{
				watch.Stop();
				_logger.LogInformation("{eventId} {eventType} processed in {elapsedTime} ms", @event.Id, @event.GetType(), watch.ElapsedMilliseconds);
			}
		}
	}
}
