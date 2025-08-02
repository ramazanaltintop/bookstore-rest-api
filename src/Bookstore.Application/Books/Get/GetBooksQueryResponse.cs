namespace Bookstore.Application.Books.Get;

public sealed record GetBooksQueryResponse(
    Guid Id,
    string Title,
    decimal Price);