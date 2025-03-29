using Microsoft.OpenApi.Models;
using TodoApp.Infrastructure.Persistence.Repository;

namespace TodoApp.API.Endpoints;

public sealed class CompleteTodoEndpoint : IEndpoint
{
    public string Pattern => "/todos/{id}/complete";
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(Pattern, Handler)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Marks a todo item as completed",
                Description = "Updates the specified todo item's status to completed",
                Tags = new List<OpenApiTag> { new() { Name = "Todos" } }
            })
            .WithName("CompleteTodo")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
    
    private async Task<IResult> Handler(Guid id, TodosRepository repository, ILogger<CompleteTodoEndpoint> logger)
    {
        var todo = await repository.GetTodoByIdAsync(id);
        
        if (todo is null)
        {
            logger.LogInformation("Todo item with ID {Id} was not found", id);
            return Results.NotFound();
        }
        
        if (todo.IsCompleted)
        {
            logger.LogInformation("Todo item with ID {Id} is already completed", id);
            return Results.NotFound();
        }
        
        todo.MarkAsCompleted();
        await repository.PatchTodoAsync(todo);
        return Results.NoContent();
    }
}