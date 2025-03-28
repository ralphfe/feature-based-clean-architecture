using TodoApp.Application.Interfaces;

namespace TodoApp.Application.Features.CompleteTodo;

public class CompleteTodoCommandHandler(ITodosRepository repository) : CommandHandler<Task<bool>, Guid>
{
    public override async Task<bool> Execute(Guid id)
    {
        var todo = await repository.GetTodoByIdAsync(id);
        
        if (todo is not null)
        {
            todo.MarkAsCompleted();
            await repository.PatchTodoAsync(todo);
            return true;
        }
        
        return false;
    }
}