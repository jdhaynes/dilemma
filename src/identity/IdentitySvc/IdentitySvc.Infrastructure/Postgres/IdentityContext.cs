using DilemmaApp.IdentitySvc.Domain.Models;
using DilemmaApp.IdentitySvc.Infrastructure.Postgres.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DilemmaApp.IdentitySvc.Infrastructure.Postgres
{
    public class IdentityContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "public";
        public DbSet<User> Users { get; set; }
        
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}