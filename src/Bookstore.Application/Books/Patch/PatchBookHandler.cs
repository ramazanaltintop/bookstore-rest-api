using Bookstore.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Ramazan.Mediator;

namespace Bookstore.Application.Books.Patch;

public sealed class PatchBookHandler(IApplicationDbContext context)
    : ICommandHandler<PatchBookCommand>
{
    public async Task Handle(
        PatchBookCommand command,
        CancellationToken cancellationToken)
    {
        var book = await context.Books
            .SingleOrDefaultAsync(b => b.Id == command.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Book could not be found");

        var dto = new PatchBookDto()
        {
            Title = book.Title,
            Price = book.Price
        };

        command.PatchDocument.ApplyTo(dto);

        book.Title = dto.Title;
        book.Price = Convert.ToDecimal(dto.Price);

        await context.SaveChangesAsync(cancellationToken);
    }
}
