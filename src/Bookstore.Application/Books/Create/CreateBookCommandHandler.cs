using Bookstore.Application.Abstractions.Data;
using Bookstore.Application.Abstractions.Messaging;
using Bookstore.Domain.Books;

namespace Bookstore.Application.Books.Create;

public interface ICreateBookCommandHandler : IHandler
{
    Task<CreateBookCommandResponse> HandleAsync(
        CreateBookCommand command,
        CancellationToken cancellationToken = default);
}

internal sealed class CreateBookCommandHandler(IApplicationDbContext context)
    : ICreateBookCommandHandler
{
    public async Task<CreateBookCommandResponse> HandleAsync(
        CreateBookCommand command,
        CancellationToken cancellationToken = default)
    {
        Book book = new()
        {
            Id = Guid.CreateVersion7(),
            ISBN = command.ISBN,
            Title = command.Title,
            Price = command.Price,
            StockQuantity = command.StockQuantity
        };

        await context.Books
            .AddAsync(book, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return new(book.Id, book.ISBN, book.Title, book.Price, book.StockQuantity);
    }
}