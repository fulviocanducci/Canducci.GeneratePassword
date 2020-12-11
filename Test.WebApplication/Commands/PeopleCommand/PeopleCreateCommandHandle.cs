using Canducci.GeneratePassword;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Test.WebApplication.Models;

namespace Test.WebApplication.Commands.PeopleCommand
{
    public class PeopleCreateCommandHandle : IRequestHandler<PeopleCreateCommand, People>
    {
        public DatabaseContext Database { get; }
        public BCrypt Crypt { get; }

        public PeopleCreateCommandHandle(DatabaseContext database, BCrypt crypt)
        {
            Database = database;
            Crypt = crypt;
        }

        public async Task<People> Handle(PeopleCreateCommand request, CancellationToken cancellationToken)
        {
            var pass = Crypt.Hash(request.Password);
            var people = new People
            {
                Name = request.Name,
                PasswordHashed = pass.Hashed,
                PasswordSalt = pass.Salt
            };
            await Database.People.AddAsync(people, cancellationToken);
            await Database.SaveChangesAsync(cancellationToken);
            return people;
        }
    }
}
