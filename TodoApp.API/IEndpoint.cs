namespace TodoApp.API;

public interface IEndpoint
{
    public string Pattern { get; }
    
    public void MapEndpoint(IEndpointRouteBuilder app);
}
