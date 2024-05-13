using MediatR.NotificationPublishers;
using MessageBus;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Users;
using Users.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Modules
var mediatRAssemblies = new List<Assembly>();
builder.Services.AddEventBus(mediatRAssemblies);
builder.Services.AddUsersModule(mediatRAssemblies);

// Add MediatR
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray());
	configuration.NotificationPublisher = new TaskWhenAllPublisher();
});

var app = builder.Build();

app.UseHttpsRedirection();


app.MapPost("User", async (
	[FromServices] IEventBus eventBus,
	[FromServices] ILogger<Program> logger) =>
{
	// Do something with the user data ...
	logger.LogInformation("User submitted.");

	// Publish event
	var userRegisteredEvent = new UserSubmittedEvent();
	await eventBus.Publish(userRegisteredEvent);

	return Results.Ok("EventId: " + userRegisteredEvent.Id);
});


app.MapPut("Cancel/{eventId}", async ([FromServices] IEventBus eventBus, Guid eventId) =>
{
	await eventBus.Publish(new CancellationEvent(eventId));
	return Results.Ok(eventId + " stopped.");
});

app.Run();