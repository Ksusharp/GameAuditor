using GameAuditor.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameAuditor.Database
{
    public class ApplicationContext : IdentityDbContext<User>
    {        
        public DbSet<Game> Games { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostTag> Tags { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Platform> Platforms { get; set; }

        public DbSet<TagNavigation> TagNavigation { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Post>().Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Post>().Property(x => x.UpdatedDate).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<PostTag>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<PostTag>().HasIndex(x => x.Tag).IsUnique();
            modelBuilder.Entity<Game>().Property(x => x.Id).HasDefaultValueSql("NEWID()"); 
            modelBuilder.Entity<TagNavigation>().Property(x => x.Id).HasDefaultValueSql("NEWID()");      
        }
    }
}
