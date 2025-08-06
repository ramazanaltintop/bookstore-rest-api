using Ramazan.Mediator;

namespace Bookstore.Application.Books.Update;

public sealed record UpdateBookCommand(
    Guid Id,
    string ISBN,
    string Title,
    decimal Price,
    int StockQuantity) : ICommand<UpdateBookCommandResponse>;

public sealed record UpdateBookDto(
    string ISBN,
    string Title,
    decimal Price,
    int StockQuantity);