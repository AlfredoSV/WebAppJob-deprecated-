using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace Persistence.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options)
        : base(options){}

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");

            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<CivilStatus> CivilStatus { get; set; }
        public DbSet<EnglishLevel> EnglishLevel { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set; }

    }
}
