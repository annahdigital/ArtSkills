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
            //Database.Migrate();
        }

        public DbSet<Art> Arts { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
