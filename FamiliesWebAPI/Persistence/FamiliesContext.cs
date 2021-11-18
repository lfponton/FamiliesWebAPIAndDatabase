using FamiliesWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FamiliesWebAPI.Persistence
{
    public class FamiliesContext : DbContext
    {
        public DbSet<Family> Families { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Adult> Adults { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Job> Jobs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                @"Data Source = C:/Users/lfpon/RiderProjects/FamiliesWebAPIAndDatabase/FamiliesWebAPI/Families.db");
        }
    }
}