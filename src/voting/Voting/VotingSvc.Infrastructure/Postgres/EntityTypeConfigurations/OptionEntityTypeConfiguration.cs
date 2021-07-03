using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VotingSvc.Domain.Dilemma.Model;

namespace VotingSvc.Infrastructure.Postgres.EntityTypeConfigurations
{
    public class OptionEntityTypeConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> config)
        {
            throw new System.NotImplementedException();
        }
    }
}