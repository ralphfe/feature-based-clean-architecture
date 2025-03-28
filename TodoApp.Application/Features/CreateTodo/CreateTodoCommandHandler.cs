using Microsoft.Extensions.Logging;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.CreateTodo;

public class CreateTodoCommandHandler(ITodosRepository context, IValidator<CreateTodoCommand> validator, ILogger<CreateTodoCommandHandler> logger) 
    : CommandHandler<Task<Result<Todo>>, CreateTodoCommand>
{
    public override async Task<Result<Todo>> Execute(CreateTodoCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsSuccess)
        {
            logger.LogError("Validation failed: {Error}", validationResult.Error);
            return validationResult.Error;
        }

        var todo = Todo.Create(command.Title, command.Description);
        await context.CreateTodoAsync(todo);
        return todo;
    }
}