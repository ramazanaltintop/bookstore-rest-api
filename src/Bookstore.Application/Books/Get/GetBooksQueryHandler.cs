using Bookstore.Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Books.Get;

internal sealed class GetBooksQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetBooksQuery, IEnumerable<GetBooksQueryResponse>>
{
    public async Task<IEnumerable<GetBooksQueryResponse>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        return await context.Books
            .Select(s => new GetBooksQueryResponse(
                s.Id,
                s.Title,
                s.Price))
            .ToListAsync(cancellationToken);
    }
}
