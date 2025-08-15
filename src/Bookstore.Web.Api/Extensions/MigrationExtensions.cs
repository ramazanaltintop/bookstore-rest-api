using Microsoft.EntityFrameworkCore;

namespace Bookstore.Web.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations<TContext>(
        this IApplicationBuilder app,
        bool allowProductionMigrations = false)
        where TContext : DbContext
    {
        var environment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
        if (environment.IsProduction() && !allowProductionMigrations)
        {
            throw new InvalidOperationException("Production migrations are disabled. Set allowProductionMigrations to true if needed.");
        }

        using IServiceScope scope = app.ApplicationServices.CreateScope();

        TContext dbContext = scope.ServiceProvider.GetRequiredService<TContext>();

        dbContext.Database.Migrate();
    }
}