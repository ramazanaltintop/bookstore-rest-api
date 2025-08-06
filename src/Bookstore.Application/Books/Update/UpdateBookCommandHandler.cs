using Bookstore.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Ramazan.Mediator;

namespace Bookstore.Application.Books.Update;

public sealed class UpdateBookCommandHandler(IApplicationDbContext context)
    : ICommandHandler<UpdateBookCommand, UpdateBookCommandResponse>
{
    public async Task<UpdateBookCommandResponse> Handle(
        UpdateBookCommand command,
        CancellationToken cancellationToken)
    {
        var book = await context.Books
            .SingleOrDefaultAsync(b => b.Id == command.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Book could not be found");

        book.ISBN = command.ISBN;
        book.Title = command.Title;
        book.Price = command.Price;
        book.StockQuantity = command.StockQuantity;

        await context.SaveChangesAsync(cancellationToken);

        return new(book.Id, book.ISBN, book.Title, book.Price, book.StockQuantity);
    }
}
