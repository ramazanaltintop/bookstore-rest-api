using Bookstore.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Ramazan.Mediator;

namespace Bookstore.Application.Books.GetById;

public sealed class GetBookByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetBookByIdQuery, GetBookByIdQueryResponse>
{
    public async Task<GetBookByIdQueryResponse> Handle(
        GetBookByIdQuery query,
        CancellationToken cancellationToken)
    {
        var book = await context.Books
            .AsNoTracking()
            .Where(b => b.IsDeleted == false)
            .SingleOrDefaultAsync(b => b.Id == query.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Book could not be found");

        return new(book.Id, book.Title, book.Price);
    }
}