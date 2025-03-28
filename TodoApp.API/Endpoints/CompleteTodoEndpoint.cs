using Microsoft.OpenApi.Models;
using TodoApp.Application.Features.CompleteTodo;

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
    
    private async Task<IResult> Handler(Guid id, CompleteTodoCommandHandler todoCommandHandler)
    {
        var res = await todoCommandHandler.Execute(id);
        return res ? Results.NoContent() : Results.NotFound();
    }
}