using Microsoft.Extensions.DependencyInjection;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Persistence.Repository;

namespace TodoApp.API.IntegrationTests.Endpoints;

public class GetAllTodosEndpointTest
{
    private readonly HttpClient client;
    private readonly TodosRepository repository;

    public GetAllTodosEndpointTest()
    {
        var factory = new WebApplicationFactory<Program>();
        this.repository = factory.Services.GetService<TodosRepository>() ?? throw new NullReferenceException(nameof(TodosRepository));
        this.client = factory.CreateClient();
    }
    
    [Fact]
    public async Task GetTodos_WithExistingItems_ReturnsOk()
    {
        // Arrange
        await this.repository.CreateTodoAsync(Todo.Create("Test", "Test"));
        
        // Act
        var response = await this.client.GetAsync("/todos");
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
