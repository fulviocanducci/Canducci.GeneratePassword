using Microsoft.EntityFrameworkCore;
using Test.WebApplication.Commands.PeopleCommand;

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

        public DbSet<Test.WebApplication.Commands.PeopleCommand.PeopleChangePasswordGetByIdCommand> PeopleChangePasswordGetByIdCommand { get; set; }
    }
}
