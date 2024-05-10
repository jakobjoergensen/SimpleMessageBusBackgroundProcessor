using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MessageBus;

public static class RegisterServices
{
	public static IServiceCollection AddMessageBusModule(this IServiceCollection services, int workerCount = 1)
	{
		services.AddSingleton<InMemoryMessageQueue>();
		services.AddSingleton<IEventBus, EventBus>();

		for (int i = 0; i < workerCount; i++)
		{
			services.AddSingleton<IHostedService, EventProcessor>();
		}

		return services;
	}
}
