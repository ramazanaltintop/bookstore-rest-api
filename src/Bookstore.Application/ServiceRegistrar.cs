using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.Application;

public static class ServiceRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration
            .RegisterServicesFromAssembly(typeof(ServiceRegistrar).Assembly));

        return services;
    }
}
