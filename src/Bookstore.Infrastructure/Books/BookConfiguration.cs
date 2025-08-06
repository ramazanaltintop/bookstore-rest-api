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

        builder.Property(b => b.ISBN)
            .HasColumnType("varchar(13)")
            .IsRequired();

        builder.HasIndex(b => b.ISBN)
            .IsUnique();

        builder.Property(b => b.Title)
            .HasColumnType("varchar(256)")
            .IsRequired();

        builder.Property(b => b.Price)
            .HasColumnType("numeric(18,2)")
            .IsRequired();

        builder.Property(b => b.StockQuantity)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(b => b.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(b => b.CreatedByUserId)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(b => b.CreatedByFullName)
            .HasColumnType("varchar(256)")
            .IsRequired();

        builder.Property(b => b.UpdatedAt)
            .HasColumnType("timestamp with time zone");

        builder.Property(b => b.UpdatedByUserId)
            .HasColumnType("uuid");

        builder.Property(b => b.UpdatedByFullName)
            .HasColumnType("varchar(256)");

        builder.Property(b => b.IsDeleted)
            .HasColumnType("boolean")
            .IsRequired();

        builder.Property(b => b.DeletedAt)
            .HasColumnType("timestamp with time zone");

        builder.Property(b => b.DeletedByUserId)
            .HasColumnType("uuid");

        builder.Property(b => b.DeletedByFullName)
            .HasColumnType("varchar(256)");
    }
}
