using DilemmaApp.IdentitySvc.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DilemmaApp.IdentitySvc.Infrastructure.Postgres.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> config)
        {
            config.ToTable("user");

            config.HasKey(x => x.Id);
            config.Ignore(x => x.DomainEvents);
            config.Ignore(x => x.IsClosed);

            config.OwnsOne(x => x.Password);
            config.OwnsOne(x => x.Name);

            config.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("id");

            config.Property(x => x.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();

            config.Property(x => x.DateOfBirth)
                .HasColumnName("dob")
                .IsRequired();

            config.Property(x => x.AccountClosed)
                .HasColumnName("account_closed");

            config.Property(x => x.AccountRegistered)
                .IsRequired()
                .HasColumnName("account_registered");

            config.OwnsOne(x => x.Name, n =>
            {
                n.Property(x => x.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsRequired();

                n.Property(x => x.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsRequired();
            });

            config.OwnsOne(x => x.Password, p =>
            {
                p.Property(x => x.Hash)
                    .HasColumnName("password")
                    .HasMaxLength(64)
                    .IsRequired();

                p.Property(x => x.Salt)
                    .HasColumnName("salt")
                    .HasMaxLength(32)
                    .IsRequired();
            });
        }
    }
}