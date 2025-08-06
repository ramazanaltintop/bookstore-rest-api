namespace Bookstore.Application.Books.Get;

public sealed record GetBooksQueryResponse(
    Guid Id,
    string ISBN,
    string Title,
    decimal Price,
    int StockQuantity);