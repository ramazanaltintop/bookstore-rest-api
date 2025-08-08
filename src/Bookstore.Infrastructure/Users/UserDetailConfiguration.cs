using Bookstore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Infrastructure.Users;

internal sealed class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
{
    public void Configure(EntityTypeBuilder<UserDetail> builder)
    {
        builder.HasKey(ui => ui.Id);

        builder.Property(ui => ui.Id)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(ui => ui.FirstName)
            .HasColumnType("varchar(128)")
            .IsRequired();

        builder.Property(ui => ui.LastName)
            .HasColumnType("varchar(128)")
            .IsRequired();

        builder.Property(ui => ui.Age)
            .HasColumnType("smallint");

        builder.Property(ui => ui.Phone)
            .HasMaxLength(20)
            .HasColumnType("varchar(20)");
    }
}
