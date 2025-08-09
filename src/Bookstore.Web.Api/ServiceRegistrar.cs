using Bookstore.Web.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;
using System.Threading.RateLimiting;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

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

        services.AddCustomRateLimiter();

        return services;
    }

    private static IServiceCollection AddCustomRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(rateLimiterOptions =>
        {
            rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            rateLimiterOptions.OnRejected = async (context, token) =>
            {
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out TimeSpan retryAfter))
                {
                    context.HttpContext.Response.Headers.RetryAfter = $"{retryAfter.TotalSeconds}";

                    ProblemDetailsFactory problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
                    ProblemDetails problemDetails = problemDetailsFactory.CreateProblemDetails(
                        context.HttpContext,
                        StatusCodes.Status429TooManyRequests,
                        "Too Many Requests",
                        detail: $"Too many requests. Please try again after {retryAfter.TotalSeconds} seconds.");

                    await context.HttpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: token);
                }
            };

            rateLimiterOptions.AddPolicy("per-user", (httpContext =>
            {
                string? userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!string.IsNullOrWhiteSpace(userId))
                {
                    return RateLimitPartition.GetTokenBucketLimiter(
                        userId,
                        _ => new TokenBucketRateLimiterOptions
                        {
                            TokenLimit = 5,
                            TokensPerPeriod = 2,
                            ReplenishmentPeriod = TimeSpan.FromMinutes(1)
                        });
                }

                return RateLimitPartition.GetFixedWindowLimiter(
                    "anonymous",
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 5,
                        Window = TimeSpan.FromMinutes(1)
                    });
            }));
        });

        return services;
    }
}