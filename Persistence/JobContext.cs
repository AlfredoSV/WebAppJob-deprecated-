using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class JobContext : DbContext
    {
        public JobContext(DbContextOptions<JobContext> options)
        : base(options)
        {
        }
        public DbSet<Area> Areas { get; set; }
    }
}
