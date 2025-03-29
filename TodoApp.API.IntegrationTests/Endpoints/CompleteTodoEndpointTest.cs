using Microsoft.Extensions.DependencyInjection;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Persistence.Repository;

namespace TodoApp.API.IntegrationTests.Endpoints;

public class CompleteTodoEndpointTest
{
    private readonly HttpClient client;
    private readonly TodosRepository repository;

    public CompleteTodoEndpointTest()
    {
        var factory = new WebApplicationFactory<Program>();
        this.repository = factory.Services.GetService<TodosRepository>() ?? throw new NullReferenceException(nameof(TodosRepository));
        this.client = factory.CreateClient();
    }

    [Fact]
    public async Task CompleteTodo_WithExistingItem_ReturnsNoContentStatus()
    {
        // Arrange
        var todo = Todo.Create("Test", "Test");
        await this.repository.CreateTodoAsync(todo);
        
        // Act
        var response = await this.client.PutAsync($"/todos/{todo.Id}/complete", null);
        
        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
