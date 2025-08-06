namespace Bookstore.Application.Books.Create;

public sealed record CreateBookCommandResponse(
    Guid Id,
    string ISBN,
    string Title,
    decimal Price,
    int StockQuantity);