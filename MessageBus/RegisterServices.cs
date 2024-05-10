using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace MessageBus;

public static class RegisterServices
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, List<Assembly> mediatrAssemblies)
    {
        services.AddSingleton<InMemoryMessageQueue>();
        services.AddSingleton<IEventBus, EventBus>();
        services.AddSingleton<EventProcessor>();

        services.AddSingleton<IHostedService>(provider => provider.GetRequiredService<EventProcessor>());
        services.AddSingleton<IEventProcessor>(provider => provider.GetRequiredService<EventProcessor>());

        mediatrAssemblies.Add(typeof(RegisterServices).Assembly);

        return services;
    }
}
