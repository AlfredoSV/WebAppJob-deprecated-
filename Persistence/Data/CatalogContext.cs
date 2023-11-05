using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options)
        : base(options)
        {
        }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<CivilStatus> CivilStatus { get; set; }
        public DbSet<EnglishLevel> EnglishLevel { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set; }

    }
}
