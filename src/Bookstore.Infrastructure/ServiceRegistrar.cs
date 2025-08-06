using Bookstore.Application.Abstractions.Authentication;
using Bookstore.Application.Abstractions.Data;
using Bookstore.Infrastructure.Authentication;
using Bookstore.Infrastructure.Database;
using Bookstore.Infrastructure.Database.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.Infrastructure;

public static class ServiceRegistrar
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddDatabase(configuration)
            .AddAuthenticationInternal()
            .AddAuthorizationInternal();
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AuditInterceptor>();

        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseNpgsql(connectionString, npgsqlOptions 
                    => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
                        .UseSnakeCaseNamingConvention();
                options.AddInterceptors(sp.GetRequiredService<AuditInterceptor>());
            });

        services.AddScoped<IApplicationDbContext>(sp => sp
            .GetRequiredService<ApplicationDbContext>());

        return services;
    }

    private static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services)
    {
        services.ConfigureOptions<JwtConfigureOptions>();
        services.ConfigureOptions<JwtPostConfigureOptions>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();

        return services;
    }

    private static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
    {
        services.AddAuthorization();

        return services;
    }
}
