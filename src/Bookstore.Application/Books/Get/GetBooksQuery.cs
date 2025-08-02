using MediatR;

namespace Bookstore.Application.Books.Get;

public sealed record GetBooksQuery : IRequest<IEnumerable<GetBooksQueryResponse>>;