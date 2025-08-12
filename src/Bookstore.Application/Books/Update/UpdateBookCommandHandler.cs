using Bookstore.Application.Abstractions.Data;
using Bookstore.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Books.Update;

public interface IUpdateBookCommandHandler : IHandler
{
    Task<UpdateBookCommandResponse> HandleAsync(
        UpdateBookCommand command,
        CancellationToken cancellationToken = default);
}

internal sealed class UpdateBookCommandHandler(IApplicationDbContext context)
    : IUpdateBookCommandHandler
{
    public async Task<UpdateBookCommandResponse> HandleAsync(
        UpdateBookCommand command,
        CancellationToken cancellationToken = default)
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
