using Canducci.GeneratePassword.Argon2id;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Test.WebApplication.Argon2id.Models;

namespace Test.WebApplication.Argon2id.Commands.PeopleCommand
{
    public class PeopleCreateCommandHandle : IRequestHandler<PeopleCreateCommand, People>
    {
        public DatabaseContext Database { get; }
        public IArgon2idPasswordHasher Crypt { get; }

        public PeopleCreateCommandHandle(DatabaseContext database, IArgon2idPasswordHasher crypt)
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

