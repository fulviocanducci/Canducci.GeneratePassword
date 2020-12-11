using Microsoft.EntityFrameworkCore;

namespace Test.WebApplication.Models
{
    public class DatabaseContext: DbContext
    {
        public DbSet<People> People { get; set; }
        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Database.db");
        }
    }
}
