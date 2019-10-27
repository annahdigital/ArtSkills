using System;
using System.Collections.Generic;
using System.Text;
using ArtSkills.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ArtSkills.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
         {
            Database.Migrate();
        }

        public DbSet<Art> Arts { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<FollowArtist> FollowArtists { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<ApplicationUser>().HasMany(a => a.Arts).WithOne(u => u.ApplicationUser);
            builder.Entity<ApplicationUser>().HasMany(a => a.Following).WithOne(u => u.Follower);
            builder.Entity<ApplicationUser>().HasMany(a => a.FollowedBy).WithOne(u => u.Artist);
            base.OnModelCreating(builder);
        }
    }
}
