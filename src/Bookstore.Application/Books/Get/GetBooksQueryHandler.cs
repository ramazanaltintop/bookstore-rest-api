using Bookstore.Application.Abstractions.Data;
using Bookstore.Application.Abstractions.Messaging;
using Bookstore.Application.Abstractions.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Books.Get;

public interface IGetBooksQueryHandler : IHandler
{
    Task<PaginationResult<GetBooksQueryResponse>> HandleAsync(
        GetBooksQuery query,
        CancellationToken cancellationToken = default);
}

internal sealed class GetBooksQueryHandler(IApplicationDbContext context)
    : IGetBooksQueryHandler
{
    public async Task<PaginationResult<GetBooksQueryResponse>> HandleAsync(
        GetBooksQuery query,
        CancellationToken cancellationToken = default)
    {
        return await context.Books
            .AsNoTracking()
            .Where(b => b.IsDeleted == false)
            .Where(b =>
                b.Title.ToLower().Contains(query.Search.ToLower()))
            .OrderBy(b => b.Title)
            .Select(s => new GetBooksQueryResponse(
                s.Id,
                s.ISBN,
                s.Title,
                s.Price,
                s.StockQuantity))
            .ToPagedListAsync(query.PageNumber, query.PageSize, cancellationToken);
    }
}