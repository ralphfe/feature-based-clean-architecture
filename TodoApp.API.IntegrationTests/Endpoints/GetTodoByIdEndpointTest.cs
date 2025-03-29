using Microsoft.Extensions.DependencyInjection;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Persistence.Repository;

namespace TodoApp.API.IntegrationTests.Endpoints;

public class GetTodoByIdEndpointTest
{
    private readonly HttpClient client;
    private readonly TodosRepository repository;

    public GetTodoByIdEndpointTest()
    {
        var factory = new WebApplicationFactory<Program>();
        this.repository = factory.Services.GetService<TodosRepository>() ?? throw new NullReferenceException(nameof(TodosRepository));
        this.client = factory.CreateClient();
    }

    [Fact]
    public async Task GetTodo_WithExistingItem_ReturnsOk()
    {
        // Arrange
        var todo = Todo.Create("Test", "Test");
        await this.repository.CreateTodoAsync(todo);
        
        // Act
        var response = await this.client.GetAsync($"/todos/{todo.Id}");
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
