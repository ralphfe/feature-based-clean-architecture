using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoApp.Infrastructure.Extensions;
using TodoApp.Infrastructure.Options;
using TodoApp.Infrastructure.Persistence;
using TodoApp.Infrastructure.Persistence.Repository;

namespace TodoApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var options = services.GetAndConfigureOptions<DbOptions>(configuration, DbOptions.Options);
        services.AddDbContext<TodosDbContext>(builder => builder.UseInMemoryDatabase(options.ConnectionString));
        
        services.AddScoped<TodosRepository>();
        return services;
    }
}
