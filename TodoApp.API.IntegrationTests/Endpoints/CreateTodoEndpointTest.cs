using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.Features;
using TodoApp.Application.Features.CreateTodo;
using TodoApp.Application.Interfaces;

namespace TodoApp.API.IntegrationTests.Endpoints;

public class CreateTodoEndpointTest
{
    private readonly HttpClient client;

    public CreateTodoEndpointTest()
    {
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
                builder.ConfigureServices(services =>
                    services.AddSingleton<ITodosRepository, TestTodosRepository>()));
        this.client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateTodo_WithCreateContent_ReturnsCreatedStatus()
    {
        // Arrange
        var todo = new CreateTodoCommand("Test", "Test");
        var content = new StringContent(JsonSerializer.Serialize(todo), System.Text.Encoding.UTF8, "application/json");
        
        // Act
        var response = await this.client.PostAsync("/todos", content);
        
        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}
