using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes{ get; set; }
        public DbSet<Photo> Photos{ get; set; }
        public DbSet<Topic> Topics{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Like>(x =>
                x.HasKey(k => new {k.AppUserId, k.TopicId}));

            builder.Entity<Like>()
                .HasOne(u => u.AppUser)
                .WithMany(t => t.Likes)
                .HasForeignKey(l => l.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);;
            
            builder.Entity<Like>()
                .HasOne(u => u.Topic)
                .WithMany(t => t.Likes)
                .HasForeignKey(l => l.TopicId)
                .OnDelete(DeleteBehavior.Cascade);;

            builder.Entity<Comment>()
                .HasOne(t => t.Topic)
                .WithMany(c => c.Comments)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>()
                .HasOne(p => p.Photo)
                .WithOne(o => o.Owner)
                .HasForeignKey<Photo>(p => p.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Topic>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Topics)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}