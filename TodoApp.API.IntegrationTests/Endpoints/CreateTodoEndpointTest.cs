using System.Text.Json;
using TodoApp.API.Endpoints;

namespace TodoApp.API.IntegrationTests.Endpoints;

public class CreateTodoEndpointTest
{
    private readonly HttpClient client;

    public CreateTodoEndpointTest()
    {
        var factory = new WebApplicationFactory<Program>();
        this.client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateTodo_WithCreateContent_ReturnsCreatedStatus()
    {
        // Arrange
        var todo = new CreateTodoEndpoint.CreateTodoRequest("Test", "Test");
        var content = new StringContent(JsonSerializer.Serialize(todo), System.Text.Encoding.UTF8, "application/json");
        
        // Act
        var response = await this.client.PostAsync("/todos", content);
        
        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}
