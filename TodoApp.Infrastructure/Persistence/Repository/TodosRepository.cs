using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Persistence.Repository;

public class TodosRepository(TodosDbContext todosDbContext) : ITodosRepository
{
    public async Task<Todo?> GetTodoByIdAsync(Guid id)
    {
        return await todosDbContext.Todos.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<IEnumerable<Todo>> GetAllTodosAsync()
    {
        return await todosDbContext.Todos.ToListAsync();
    }

    public async Task CreateTodoAsync(Todo todo)
    {
        await todosDbContext.Todos.AddAsync(todo);
        await todosDbContext.SaveChangesAsync();
    }
    
    public async Task PatchTodoAsync(Todo todo)
    {
        todosDbContext.Todos.Update(todo);
        await todosDbContext.SaveChangesAsync();
    }
}
