﻿using Domain.Entities;
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
                .HasForeignKey(l => l.AppUserId);
            
            builder.Entity<Like>()
                .HasOne(u => u.Topic)
                .WithMany(t => t.Likes)
                .HasForeignKey(l => l.TopicId);

            builder.Entity<Comment>()
                .HasOne(t => t.Topic)
                .WithMany(c => c.Comments)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}