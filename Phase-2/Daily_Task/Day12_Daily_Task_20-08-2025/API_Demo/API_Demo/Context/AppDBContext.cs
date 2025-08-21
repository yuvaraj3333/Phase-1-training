using API_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Demo.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }

        public DbSet<BookModel> BookDB { get; set; }
        public DbSet<UserModel> UserDB { get; set; }
    }
}
