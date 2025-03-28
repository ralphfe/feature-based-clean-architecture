using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.Features;

namespace TodoApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        DependencyInjection.AddHandlers(services);
        DependencyInjection.AddValidators(services);
        return services;
    }

    public static void AddHandlers(IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        var handlerTypes = assembly.GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .Where(t => t.Name.EndsWith("Handler"))
            .Where(t => t.BaseType?.IsGenericType == true && 
                       (t.BaseType.GetGenericTypeDefinition() == typeof(QueryHandler<>) ||
                        t.BaseType.GetGenericTypeDefinition() == typeof(QueryHandler<,>) ||
                        t.BaseType.GetGenericTypeDefinition() == typeof(CommandHandler<,>)));

        foreach (var handlerType in handlerTypes)
        {
            services.AddScoped(handlerType);
        }
    }

    public static void AddValidators(IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        var validatorTypes = assembly.GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)));

        foreach (var validatorType in validatorTypes)
        {
            var interfaceType = validatorType.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>));
            services.AddScoped(interfaceType, validatorType);
        }
    }
}
