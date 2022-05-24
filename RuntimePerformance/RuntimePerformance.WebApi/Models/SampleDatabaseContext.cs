using Microsoft.EntityFrameworkCore;
using RuntimePerformance.Shared.Models;

namespace RuntimePerformance.WebApi.Models
{
    public class SampleDatabaseContext : DbContext
    {
        public SampleDatabaseContext(DbContextOptions<SampleDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Conference> Conferences { get; set; } = default!;
        public DbSet<Contribution> Contributions { get; set; } = default!;
        public DbSet<Speaker> Speakers { get; set; } = default!;
    }
}
