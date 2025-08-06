using Ramazan.Mediator;

namespace Bookstore.Application.Books.GetById;

public sealed record GetBookByIdQuery(Guid Id) : IQuery<GetBookByIdQueryResponse>;