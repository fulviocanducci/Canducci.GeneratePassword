using Microsoft.EntityFrameworkCore;
using Test.WebApplication.Argon2id.Commands.PeopleCommand;

namespace Test.WebApplication.Argon2id.Models
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

        public DbSet<Test.WebApplication.Argon2id.Commands.PeopleCommand.PeopleChangePasswordGetByIdCommand> PeopleChangePasswordGetByIdCommand { get; set; }
    }
}

