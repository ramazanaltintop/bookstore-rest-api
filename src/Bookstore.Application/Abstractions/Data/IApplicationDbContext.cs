using Bookstore.Domain.Books;
using Bookstore.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Book> Books { get; }
    DbSet<User> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
