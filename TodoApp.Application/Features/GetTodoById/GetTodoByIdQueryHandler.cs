using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.GetTodoById;

public class GetTodoByIdQueryHandler(ITodosRepository context) : QueryHandler<Task<Todo?>, Guid>
{
    public override async Task<Todo?> Execute(Guid id)
    {
        return await context.GetTodoByIdAsync(id);
    }
}
