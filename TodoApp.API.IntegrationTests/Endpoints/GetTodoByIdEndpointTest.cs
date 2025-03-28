using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.API.IntegrationTests.Endpoints;

public class GetTodoByIdEndpointTest
{
    private readonly HttpClient client;
    private readonly TestTodosRepository repository;

    public GetTodoByIdEndpointTest()
    {
        this.repository = new TestTodosRepository();
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
                builder.ConfigureServices(services =>
                    services.AddSingleton<ITodosRepository>(this.repository)));
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
