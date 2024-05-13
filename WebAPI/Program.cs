using JNJ.MessageBus;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEventBus();

// Add MediatR
builder.Services.AddMediatR(configuration =>
{
	configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
	configuration.NotificationPublisher = new TaskWhenAllPublisher();
});

var app = builder.Build();


app.UseHttpsRedirection();

app.MapPost("User", async (
	[FromServices] IEventBus eventBus,
	[FromBody] User user) =>
{
	var userRegisteredEvent = new UserSubmittedEvent(user);
	await eventBus.Publish(userRegisteredEvent);

	return Results.Ok(new
	{
		EventId = userRegisteredEvent.Id,
        userRegisteredEvent.CancellationId
	});
});


app.MapPut("Cancel/{cancellationId}", async ([FromServices] IMediator mediator, Guid cancellationId) =>
{
	await mediator.Send(new CancelCommand(cancellationId));
	return Results.Ok();
});

app.Run();