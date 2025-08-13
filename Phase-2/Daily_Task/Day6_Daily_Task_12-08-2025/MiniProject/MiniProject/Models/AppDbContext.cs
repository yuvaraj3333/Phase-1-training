using Microsoft.EntityFrameworkCore;

namespace MovieBookingApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Audience> Audiences { get; set; }
    }
}
