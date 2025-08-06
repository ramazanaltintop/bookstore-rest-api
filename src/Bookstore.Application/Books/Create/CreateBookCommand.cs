using Ramazan.Mediator;

namespace Bookstore.Application.Books.Create;

public sealed record CreateBookCommand(
    string ISBN,
    string Title,
    decimal Price,
    int StockQuantity) : ICommand<CreateBookCommandResponse>;