namespace Bookstore.Application.Books.GetById;

public sealed record GetBookByIdQueryResponse(
    Guid Id,
    string Title,
    decimal Price);