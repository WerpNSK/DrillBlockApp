using DrillBlockApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DrillBlockApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<DrillBlock> DrillBlocks { get; set; }
        public DbSet<Hole> Holes { get; set; }
        public DbSet<HolePoints> HolePoints { get; set; }
        public DbSet<DrillBlockPoints> DrillBlockPoints { get; set; }
    }
}
