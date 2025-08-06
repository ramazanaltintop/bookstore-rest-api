using Bookstore.Web.Api.Infrastructure;

namespace Bookstore.Web.Api;

public static class ServiceRegistrar
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddProblemDetails(configure =>
        {
            configure.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
            };
        });
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddOpenApi();

        services.AddControllers()
            .AddNewtonsoftJson();

        return services;
    }
}