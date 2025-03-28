using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.API.IntegrationTests.Endpoints;

public class CompleteTodoEndpointTest
{
    private readonly HttpClient client;
    private readonly TestTodosRepository repository;

    public CompleteTodoEndpointTest()
    {
        this.repository = new TestTodosRepository();
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
                builder.ConfigureServices(services =>
                    services.AddSingleton<ITodosRepository>(this.repository)));
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
