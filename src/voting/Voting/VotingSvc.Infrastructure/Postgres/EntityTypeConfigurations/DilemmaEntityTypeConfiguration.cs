using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VotingSvc.Domain.Dilemma.Model;

namespace VotingSvc.Infrastructure.Postgres.EntityTypeConfigurations
{
    public class DilemmaEntityTypeConfiguration : IEntityTypeConfiguration<Dilemma>
    {
        public void Configure(EntityTypeBuilder<Dilemma> config)
        {
            config.ToTable("dilemma", VotingContext.DEFAULT_SCHEMA);

            config.HasKey(d => d.Id);

            config.Ignore(d => d.HasAlreadyVoted);
            config.Ignore(d => d.HasBeenOpen);
            config.Ignore(d => d.IsCurrentlyOpen);

            config.Property(d => d.Opened)
                .HasColumnName("opened");

            config.Property(d => d.Closed)
                .HasColumnName("closed");
        }
    }
}