using Microsoft.EntityFrameworkCore;
using HelloWorld.Models;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Models.Data
{
    public class DataContextEF : DbContext
    {


            private IConfiguration _config;
        
            public DataContextEF(IConfiguration config)
            {
                _config = config;

            }


        public DbSet<Computer>? Computer {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured) 
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                options => options.EnableRetryOnFailure());
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<Computer>()
            .HasKey(c => c.ComputerId);
        }
        
    }
}