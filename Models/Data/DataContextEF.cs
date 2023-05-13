using Microsoft.EntityFrameworkCore;
using HelloWorld.Models;

namespace HelloWorld.Models.Data
{
    public class DataContextEF : DbContext
    {


        public DbSet<Computer>? Computer {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured) 
            {
                options.UseSqlServer("Server=localhost;Database=DotnetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=false;User id=sa;Password=SQLConnect1;",
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