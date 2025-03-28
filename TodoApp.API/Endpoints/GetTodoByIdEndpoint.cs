using Microsoft.OpenApi.Models;
using TodoApp.Application.Features.GetTodoById;
using TodoApp.Domain.Entities;

namespace TodoApp.API.Endpoints;

public class GetTodoByIdEndpoint : IEndpoint
{
    public string Pattern => "/todos/{id}";
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Pattern, Handler)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Gets a todo item by ID",
                Description = "Retrieves a specific todo item using its unique identifier",
                Tags = new List<OpenApiTag> { new() { Name = "Todos" } },
            })
            .WithName("GetTodoById")
            .Produces<Todo>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
    
    private async Task<IResult> Handler(GetTodoByIdQueryHandler getTodoByIdQuery, Guid id)
    {
        var todo = await getTodoByIdQuery.Execute(id);
        return todo is not null ? Results.Ok(todo) : Results.NotFound();
    }
}