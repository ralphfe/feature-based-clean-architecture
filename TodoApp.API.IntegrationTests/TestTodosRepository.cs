using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.API.IntegrationTests;

public class TestTodosRepository : ITodosRepository
{
    private readonly Dictionary<Guid, Todo> todos = new();

    public Task<Todo?> GetTodoByIdAsync(Guid id)
    {
        this.todos.TryGetValue(id, out var todo);
        return Task.FromResult(todo);
    }
    
    public Task<IEnumerable<Todo>> GetAllTodosAsync()
    {
        return Task.FromResult(this.todos.Values.AsEnumerable());
    }

    public Task CreateTodoAsync(Todo todo)
    {
        this.todos[todo.Id] = todo;
        return Task.CompletedTask;
    }
    
    public Task PatchTodoAsync(Todo todo)
    {
        this.todos[todo.Id] = todo;
        return Task.CompletedTask;
    }
}