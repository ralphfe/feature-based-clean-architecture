using Microsoft.OpenApi.Models;
using TodoApp.Application.Features;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Persistence.Repository;

namespace TodoApp.API.Endpoints;

public class CreateTodoEndpoint : IEndpoint, IValidator<CreateTodoEndpoint.CreateTodoRequest>
{
    public record CreateTodoRequest(string Title, string? Description);
    
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
    
    private async Task<IResult> Handler(CreateTodoRequest request, TodosRepository repository, ILogger<CreateTodoEndpoint> logger)
    {
        var validationResult = this.Validate(request);
        if (!validationResult.IsSuccess)
        {
            logger.LogError("Validation failed: {Error}", validationResult.Error);
            return Results.BadRequest(new { error = validationResult.Error });
        }

        var todo = Todo.Create(request.Title, request.Description);
        await repository.CreateTodoAsync(todo);
        return Results.Created(new Uri($"{Pattern}/{todo.Id}", UriKind.Relative), todo.Id);
    }
    
    public Result<CreateTodoRequest> Validate(CreateTodoRequest entity)
    {
        if (string.IsNullOrEmpty(entity.Title))
            return new ValidationException("Title is required");
        
        if (entity.Title.Length > 64)
            return new ValidationException("Title cannot be longer than 64 characters");
        
        if (entity.Description?.Length > 256)
            return new ValidationException("Description cannot be longer than 256 characters");

        return entity;
    }
}