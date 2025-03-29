using Microsoft.Extensions.Logging;
using TodoApp.Application.Interfaces;

namespace TodoApp.Application.Features.CompleteTodo;

public class CompleteTodoCommandHandler(ITodosRepository repository, ILogger<CompleteTodoCommandHandler> logger) : CommandHandler<Task<bool>, Guid>
{
    public override async Task<bool> Execute(Guid id)
    {
        var todo = await repository.GetTodoByIdAsync(id);
        
        if (todo is null)
        {
            logger.LogInformation("Todo item with ID {Id} was not found", id);
            return false;
        }
        
        if (todo.IsCompleted)
        {
            logger.LogInformation("Todo item with ID {Id} is already completed", id);
            return false;
        }
        
        todo.MarkAsCompleted();
        await repository.PatchTodoAsync(todo);
        return true;
    }
}