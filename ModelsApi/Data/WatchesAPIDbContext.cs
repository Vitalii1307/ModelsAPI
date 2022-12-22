using Microsoft.EntityFrameworkCore;
using ModelsApi.Models;

namespace ModelsApi.Data
{
    public class WatchesAPIDbContext : DbContext
    {
        public WatchesAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Watch> Watches { get; set; }
    }
}
