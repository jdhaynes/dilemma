using System.Threading;
using DilemmaApp.Services.Dilemma.Infrastructure.Postgres.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using DilemmaApp.Services.Dilemma.Domain.Dilemma.Model;

namespace DilemmaApp.Services.Dilemma.Infrastructure.Postgres
{
    public class DilemmaContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "public";
        public DbSet<Domain.Dilemma.Model.Dilemma> Dilemmas { get; set; }
        public DbSet<Option> Options { get; set; }

        public DilemmaContext(DbContextOptions<DilemmaContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DilemmaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OptionEntityTypeConfiguration());
        }
    }
}