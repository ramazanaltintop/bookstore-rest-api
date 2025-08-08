using Bookstore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Infrastructure.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(b => b.Email)
            .HasColumnType("varchar(256)")
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(b => b.PasswordHash)
            .HasColumnType("varchar(128)")
            .IsRequired();
    }
}
