using Bookstore.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Ramazan.Mediator;

namespace Bookstore.Application.Books.Get;

public sealed class GetBooksQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetBooksQuery, IEnumerable<GetBooksQueryResponse>>
{
    public async Task<IEnumerable<GetBooksQueryResponse>> Handle(
        GetBooksQuery query,
        CancellationToken cancellationToken)
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