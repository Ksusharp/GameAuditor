using GameAuditor.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameAuditor.Database
{
    public class ApplicationContext : IdentityDbContext
    {        
        public DbSet<Game> Games { get; set; }
        public DbSet<Post> Posts { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }
    }
}
