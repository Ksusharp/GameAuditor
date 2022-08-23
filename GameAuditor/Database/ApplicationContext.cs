using GameAuditor.Models;
using Microsoft.EntityFrameworkCore;

namespace GameAuditor.Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { 
            Database.EnsureCreated();
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
