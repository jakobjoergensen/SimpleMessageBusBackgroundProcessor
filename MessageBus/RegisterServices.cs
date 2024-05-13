using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MessageBus;

public static class RegisterServices
{
    public static IServiceCollection AddEventBus(this IServiceCollection services)
    {
        services.AddSingleton<InMemoryMessageQueue>();
        services.AddSingleton<IEventBus, EventBus>();
        services.AddSingleton<EventProcessor>();

        services.AddSingleton<IHostedService>(provider => provider.GetRequiredService<EventProcessor>());
        services.AddSingleton<IEventProcessor>(provider => provider.GetRequiredService<EventProcessor>());

        services.AddTransient<IRequestHandler<CancelCommand>, CancelCommandHandler>();

        return services;
    }
}
