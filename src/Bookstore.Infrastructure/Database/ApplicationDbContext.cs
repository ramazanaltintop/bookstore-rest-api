using Bookstore.Application.Abstractions.Data;
using Bookstore.Domain.Books;
using Bookstore.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Database;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.HasDefaultSchema(Schemas.Default);
    }
}
