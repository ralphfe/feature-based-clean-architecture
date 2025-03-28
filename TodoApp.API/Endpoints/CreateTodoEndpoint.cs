using Microsoft.OpenApi.Models;
using TodoApp.Application.Features.CreateTodo;

namespace TodoApp.API.Endpoints;

public class CreateTodoEndpoint : IEndpoint
{
    public string Pattern => "/todos";
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Pattern, Handler)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Creates a new todo item",
                Description = "Creates a new todo item with the specified title and description",
                Tags = new List<OpenApiTag> { new() { Name = "Todos" } }
            })
            .WithName("CreateTodo")
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
    }
    
    private async Task<IResult> Handler(CreateTodoCommandHandler todoCommandHandler, CreateTodoCommand command)
    {
        var result = await todoCommandHandler.Execute(command);
        
        if (result.Value is not null)
        {
            return Results.Created(new Uri($"{Pattern}/{result.Value.Id}"), result.Value.Id);
        }
        
        return Results.BadRequest(new { error = result.Error });
    }
}