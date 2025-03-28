using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.API.IntegrationTests.Endpoints;

public class GetAllTodosEndpointTest
{
    private readonly HttpClient client;
    private readonly TestTodosRepository repository;

    public GetAllTodosEndpointTest()
    {
        this.repository = new TestTodosRepository();
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
                builder.ConfigureServices(services =>
                    services.AddSingleton<ITodosRepository>(this.repository)));
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
