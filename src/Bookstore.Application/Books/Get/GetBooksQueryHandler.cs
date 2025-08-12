using Bookstore.Application.Abstractions.Data;
using Bookstore.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Books.Get;

public interface IGetBooksQueryHandler : IHandler
{
    Task<IEnumerable<GetBooksQueryResponse>> HandleAsync(
        GetBooksQuery query,
        CancellationToken cancellationToken = default);
}

internal sealed class GetBooksQueryHandler(IApplicationDbContext context)
    : IGetBooksQueryHandler
{
    public async Task<IEnumerable<GetBooksQueryResponse>> HandleAsync(
        GetBooksQuery query,
        CancellationToken cancellationToken = default)
    {
        return await context.Books
            .AsNoTracking()
            .Where(b => b.IsDeleted == false)
            .Select(s => new GetBooksQueryResponse(
                s.Id,
                s.ISBN,
                s.Title,
                s.Price,
                s.StockQuantity))
            .ToListAsync(cancellationToken);
    }
}