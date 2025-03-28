using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces;

public interface ITodosRepository
{
    Task<Todo?> GetTodoByIdAsync(Guid id);
    
    Task<IEnumerable<Todo>> GetAllTodosAsync();
    
    Task CreateTodoAsync(Todo todo);
    
    Task PatchTodoAsync(Todo todo);
}
