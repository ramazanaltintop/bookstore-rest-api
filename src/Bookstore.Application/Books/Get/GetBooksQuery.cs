using Ramazan.Mediator;

namespace Bookstore.Application.Books.Get;

public sealed record GetBooksQuery : IQuery<IEnumerable<GetBooksQueryResponse>>;