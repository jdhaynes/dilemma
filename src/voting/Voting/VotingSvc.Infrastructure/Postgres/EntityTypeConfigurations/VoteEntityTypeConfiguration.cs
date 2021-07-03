using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VotingSvc.Domain.Dilemma.Model;

namespace VotingSvc.Infrastructure.Postgres.EntityTypeConfigurations
{
    public class VoteEntityTypeConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> config)
        {
            config.ToTable("vote", VotingContext.DEFAULT_SCHEMA);

            config.HasKey(v => new {v.DilemmaId, v.VoterId});
            
            config.Property(v => v.DilemmaId)
                .IsRequired()
                .HasColumnName("dilemma_id");
            
            config.Property(v => v.VoterId)
                .IsRequired()
                .HasColumnName("voter_id");
            
            config.Property(v => v.OptionId)
                .IsRequired()
                .HasColumnName("option_id");
            
        }
    }
}