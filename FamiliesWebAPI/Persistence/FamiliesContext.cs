using FamiliesWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FamiliesWebAPI.Persistence
{
    public class FamiliesContext : DbContext
    {
        public DbSet<Family> Families { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                @"Data Source = C:/Users/lfpon/RiderProjects/FamiliesWebAPIAndDatabase/FamiliesWebAPI/Families.db");
        }
    }
}