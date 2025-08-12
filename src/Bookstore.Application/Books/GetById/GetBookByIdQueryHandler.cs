using Bookstore.Application.Abstractions.Data;
using Bookstore.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Books.GetById;

public interface IGetBookByIdQueryHandler : IHandler
{
    Task<GetBookByIdQueryResponse> HandleAsync(
        GetBookByIdQuery query,
        CancellationToken cancellationToken = default);
}

internal sealed class GetBookByIdQueryHandler(IApplicationDbContext context)
    : IGetBookByIdQueryHandler
{
    public async Task<GetBookByIdQueryResponse> HandleAsync(
        GetBookByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var book = await context.Books
            .AsNoTracking()
            .Where(b => b.IsDeleted == false)
            .SingleOrDefaultAsync(b => b.Id == query.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Book could not be found");

        return new(book.Id, book.Title, book.Price);
    }
}