using Bookstore.Application.Abstractions.Authentication;
using Bookstore.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Bookstore.Infrastructure.Database.Interceptors;

public class AuditInterceptor(
    IUserContext userContext) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        ApplyAudit(eventData.Context!);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAudit(eventData.Context!);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplyAudit(DbContext context)
    {
        var userId = userContext.UserId;
        var fullName = userContext.FullName;

        var entries = context.ChangeTracker.Entries<Entity>();

        foreach(var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedByUserId = (Guid)userId!;
                    entry.Entity.CreatedByFullName = fullName!;
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedByUserId = userId;
                    entry.Entity.UpdatedByFullName = fullName;
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedByUserId = userId;
                    entry.Entity.DeletedByFullName = fullName;
                    entry.Entity.DeletedAt = DateTimeOffset.UtcNow;
                    break;
            }
        }
    }
}
