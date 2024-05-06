using MessageBus;
using MessageBus.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Users;
using Users.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Register the message bus
int workerCount = 10;
builder.Services.AddMessageBusModule(workerCount);

// Add MediatR
var mediatRAssemblies = new List<Assembly>();
builder.Services.AddUsersModule(mediatRAssemblies);
builder.Services.AddMediatR(configuration =>
{
	configuration.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray());
});

var app = builder.Build();


app.UseHttpsRedirection();


app.MapPost("/Users/Register", async (
	[FromServices] IEventBus eventBus,
	[FromServices] ILogger<Program> logger,
	[FromBody] User user) =>
{
	// Do something with the user data ...
	logger.LogInformation("Registration recieved. UserName: '{username}', Email: '{email}'", user.UserName, user.Email);

	// Publish event
	var userRegisteredEvent = new UserRegisteredEvent(user.UserName, user.Email);
	await eventBus.Publish(userRegisteredEvent);

	return Results.Ok();
});

app.Run();