using Bookstore.Application.Abstractions.Messaging;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bookstore.Application;

public static class ServiceRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ServiceRegistrar).Assembly);

        services.RegisterHandlers(typeof(ServiceRegistrar).Assembly);

        return services;
    }

    private static IServiceCollection RegisterHandlers(
        this IServiceCollection services,
        Assembly assembly)
    {
        var handlerTypes = assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } 
                && t.IsAssignableTo(typeof(IHandler)))
            .ToList();

        foreach (var implementationType in handlerTypes)
        {
            var interfaceType = implementationType.GetInterfaces()
                .FirstOrDefault(i => i != typeof(IHandler)
                && i.IsAssignableTo(typeof(IHandler)));

            if (interfaceType is not null)
            {
                services.AddScoped(interfaceType, implementationType);
            }
        }

        return services;
    }
}