namespace Bookstore.Application.Books.Update;

public sealed record UpdateBookCommandResponse(
    Guid Id,
    string ISBN,
    string Title,
    decimal Price,
    int StockQuantity);