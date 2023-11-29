using Microsoft.EntityFrameworkCore;

namespace WDIOU_WEB_API.Models
{
    public class WDIOUDbContext : DbContext
    {
        public WDIOUDbContext( DbContextOptions<WDIOUDbContext> options ) : base(options)
        {
             
        }

        public DbSet<User> Users { get; set; } = null!;
    }
}
