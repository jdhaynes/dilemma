using Microsoft.EntityFrameworkCore;
using VotingSvc.Domain.Dilemma.Model;
using VotingSvc.Infrastructure.Postgres.EntityTypeConfigurations;

namespace VotingSvc.Infrastructure.Postgres
{
    public class VotingContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "public";
        
        public DbSet<Dilemma> Dilemmas { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Vote> Votes { get; set; }
        
        public VotingContext(DbContextOptions<VotingContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DilemmaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OptionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VoteEntityTypeConfiguration());
        }
    }
}