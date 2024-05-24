using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace JNJ.MessageBus;

internal class EventProcessor : BackgroundService, IEventProcessor
{
    private readonly InMemoryMessageQueue _queue;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<EventProcessor> _logger;
    private ConcurrentDictionary<Guid, (Task Task, CancellationTokenSource CancellationTokenSource)> _cancelTaskDictionary = new();

    public EventProcessor(InMemoryMessageQueue queue, IServiceScopeFactory serviceScopeFactory, ILogger<EventProcessor> logger)
    {
        _queue = queue;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (Event @event in _queue.Reader.ReadAllAsync(stoppingToken))
        {
            var localCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
            var task = Task.Run(async () =>
            {
                var watch = Stopwatch.StartNew();

                try
                {
                    _logger.LogInformation("{eventId} {eventType} processing ...", @event.Id, @event.GetType());

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
                        await publisher.Publish(@event, localCancellationTokenSource.Token);
                    }
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Operation was canceled. Event {eventId} CancellationId {cancellationId}", @event.Id, @event.CancellationId);
                    throw; // Ensure the task is marked as canceled
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
            }, localCancellationTokenSource.Token);

            
            // Add the cancellationTokenSource to the dictionary
            if (!_cancelTaskDictionary.TryAdd(@event.CancellationId, (task, localCancellationTokenSource)))
            {
                _logger.LogWarning("CancellationId {CancellationId} could not be added to the dictionary.", @event.CancellationId);
            }


            // Remove the cancellationTokenSource to the dictionary when task is completed
#pragma warning disable CS4014 // Disable warning: "Because this call is not awaited, execution of the current method continues before the call is completed".
            task.ContinueWith(t => {

                if (_cancelTaskDictionary.TryRemove(@event.CancellationId, out var value))
                {
                    value.CancellationTokenSource.Dispose();
                }
                else
                {
                    _logger.LogWarning("CancellationId {CancellationId} was not found in the dictionary, might have been completed and removed already.", @event.CancellationId);
                }
            }, TaskScheduler.Default);
#pragma warning restore CS4014
        }
    }


    public void CancelTask(Guid cancellationId)
    {
        if (_cancelTaskDictionary.TryGetValue(cancellationId, out var task))
        {
            task.CancellationTokenSource.Cancel();
        }
        else
        {
            _logger.LogWarning("CancellationId {CancellationId} was not found in the dictionary, might have been completed and removed already.", cancellationId);
        }
            

    }
}
