using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.OptionsModel;

namespace HabraQuest.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<QuestTask> Tasks { get; set; }
        public DbSet<Progress> Progress { get; set; }


        private static bool _created = false;
        public ApplicationDbContext()
        {            
            // Create the database and schema if it doesn't exist
            // This is a temporary workaround to create database until Entity Framework database migrations 
            // are supported in ASP.NET 5
            if (!_created)
            {
                var xx = Database.AsMigrationsEnabled();

                Database.AsMigrationsEnabled().ApplyMigrations();
                _created = true;
            }
        }
        
        protected override void OnConfiguring(DbContextOptions options)
        {
            options.UseSqlServer(Startup.Configuration.Get("Data:DefaultConnection:ConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}