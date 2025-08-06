using Bookstore.Application.Abstractions.Data;
using Bookstore.Domain.Books;
using Ramazan.Mediator;

namespace Bookstore.Application.Books.Create;

public sealed class CreateBookCommandHandler(IApplicationDbContext context)
    : ICommandHandler<CreateBookCommand, CreateBookCommandResponse>
{
    public async Task<CreateBookCommandResponse> Handle(
        CreateBookCommand command,
        CancellationToken cancellationToken)
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
