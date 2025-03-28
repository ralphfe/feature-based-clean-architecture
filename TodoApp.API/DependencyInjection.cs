using TodoApp.API.Endpoints;

namespace TodoApp.API;

public static class DependencyInjection
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, bool isDevelopment)
    {
        if (isDevelopment)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        
        return services;
    }
    
    public static WebApplication UseSwagger(this WebApplication app, bool isDevelopment)
    {
        if (isDevelopment)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        return app;
    }
    
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        var assembly = typeof(IEndpoint).Assembly;
        
        var endpointTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Where(t => typeof(IEndpoint).IsAssignableFrom(t));

        foreach (var endpointType in endpointTypes)
        {
            var endpoint = (IEndpoint)Activator.CreateInstance(endpointType)!;
            endpoint.MapEndpoint(app);
        }
    }
}
