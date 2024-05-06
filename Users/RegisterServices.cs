using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Users;

public static class RegisterServices
{
	public static IServiceCollection AddUsersModule(this IServiceCollection services, List<Assembly> mediatRAssemblies)
	{
		mediatRAssemblies.Add(typeof(RegisterServices).Assembly);
		return services;
	}
}
