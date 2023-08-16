using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class JobContext : DbContext
    {
        public JobContext(DbContextOptions<JobContext> options) : base(options)
        { }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<ApplyCompetitorJob> CompetitorJobs { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Competitor> Competitors { get; set; }
        public DbSet<ExperiencieWork> ExperiencieWorks { get; set; }

    }
}
