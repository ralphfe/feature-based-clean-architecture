using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.GetAllTodos;

public class GetAllTodosQueryHandler(ITodosRepository context) : QueryHandler<Task<IEnumerable<Todo>>>
{
    public override async Task<IEnumerable<Todo>> Execute()
    {
        return await context.GetAllTodosAsync();
    }
}