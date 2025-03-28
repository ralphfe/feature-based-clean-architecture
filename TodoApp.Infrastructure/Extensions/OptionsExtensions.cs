using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApp.Infrastructure.Extensions;

public static class OptionsExtensions
{
    public static T GetAndConfigureOptions<T>(this IServiceCollection services, IConfiguration configuration, string sectionName) where T : class, new()
    {
        services.ConfigureOptions<T>(configuration, sectionName);
        return configuration.GetOptions<T>(sectionName);
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }

    public static void ConfigureOptions<T>(this IServiceCollection services, IConfiguration configuration, string sectionName) where T : class, new()
    {
        services.Configure<T>(configuration.GetSection(sectionName));
    }
}