using Bookstore.Domain.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Infrastructure.Books;

internal sealed class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(b => b.Title)
            .HasColumnType("varchar(256)")
            .IsRequired();

        builder.Property(b => b.Price)
            .HasColumnType("numeric(18,2)")
            .IsRequired();

        builder.HasData(
            new Book
            {
                Id = Guid.Parse("0b9b6ce9-9eb0-497b-90c7-2e5be345f139"),
                Title = "Clean Architecture",
                Price = 675
            },
            new Book
            {
                Id = Guid.Parse("9e9a1fa2-9c28-4404-bcba-f81c6f70264b"),
                Title = "Onion Architecture",
                Price = 590
            },
            new Book
            {
                Id = Guid.Parse("19d8fb37-087f-45a3-b261-683261249c3f"),
                Title = "Vertical Slice Architecture",
                Price = 450
            }
        );
    }
}
