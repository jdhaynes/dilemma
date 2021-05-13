using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DilemmaApp.Services.Dilemma.Infrastructure.Postgres.EntityConfigurations
{
    public class
        DilemmaEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Dilemma.Model.Dilemma>
    {
        public void Configure(EntityTypeBuilder<Domain.Dilemma.Model.Dilemma> config)
        {
            config.ToTable("dilemma", DilemmaContext.DEFAULT_SCHEMA);

            config.HasKey(d => d.Id);
            config.Ignore(d => d.DomainEvents);
            config.Ignore(d => d.OptionCount);
            config.Ignore(d => d.IsWithdrawn);

            config.HasMany(d => d.Options)
                .WithOne();

            config.Navigation(d => d.Options)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasField("_options");

            config.Property(d => d.Id)
                .HasColumnName("id")
                .IsRequired();

            config.Property(d => d.TopicId)
                .HasColumnName("topic_id")
                .IsRequired();

            config.Property(d => d.PosterId)
                .HasColumnName("poster_id")
                .IsRequired();

            config.Property(d => d.Question)
                .HasColumnName("question")
                .IsRequired()
                .HasMaxLength(140);

            config.Property(d => d.PostedDate)
                .HasColumnName("posted_date")
                .IsRequired();

            config.Property(d => d.WithdrawnDate)
                .HasColumnName("withdrawn_date");
        }
    }
}