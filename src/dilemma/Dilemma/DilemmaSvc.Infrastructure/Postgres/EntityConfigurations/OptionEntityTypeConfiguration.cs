using DilemmaApp.Services.Dilemma.Domain.Dilemma.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DilemmaApp.Services.Dilemma.Infrastructure.Postgres.EntityConfigurations
{
    public class OptionEntityTypeConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> config)
        {
            config.ToTable("option", DilemmaContext.DEFAULT_SCHEMA);
            
            config.HasKey(o => o.Id);
            
            config.Property(o => o.Id)
                .HasColumnName("id")
                .IsRequired();
            
            config.Property(o => o.DilemmaId)
                .HasColumnName("dilemma_id")
                .IsRequired();
            
            config.Property(o => o.Description)
                .HasColumnName("description")
                .HasMaxLength(40)
                .IsRequired();
        }
    }
}