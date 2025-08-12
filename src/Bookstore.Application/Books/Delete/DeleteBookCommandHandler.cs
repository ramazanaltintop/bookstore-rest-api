using Bookstore.Application.Abstractions.Data;
using Bookstore.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Books.Delete;

public interface IDeleteBookCommandHandler : IHandler
{
    Task HandleAsync(
        DeleteBookCommand command,
        CancellationToken cancellationToken = default);
}

internal sealed class DeleteBookCommandHandler(IApplicationDbContext context)
    : IDeleteBookCommandHandler
{
    public async Task HandleAsync(
        DeleteBookCommand command,
        CancellationToken cancellationToken = default)
    {
        var book = await context.Books
            .SingleOrDefaultAsync(b => b.Id == command.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Book could not be found");

        context.Books.Remove(book);

        await context.SaveChangesAsync(cancellationToken);
    }
}