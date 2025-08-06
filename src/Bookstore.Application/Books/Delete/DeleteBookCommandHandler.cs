using Bookstore.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Ramazan.Mediator;

namespace Bookstore.Application.Books.Delete;

public sealed class DeleteBookCommandHandler(IApplicationDbContext context)
    : ICommandHandler<DeleteBookCommand>
{
    public async Task Handle(
        DeleteBookCommand command,
        CancellationToken cancellationToken)
    {
        var book = await context.Books
            .SingleOrDefaultAsync(b => b.Id == command.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Book could not be found");

        context.Books.Remove(book);

        await context.SaveChangesAsync(cancellationToken);
    }
}