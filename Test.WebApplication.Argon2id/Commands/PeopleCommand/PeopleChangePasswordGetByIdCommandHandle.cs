using Canducci.GeneratePassword.Argon2id;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Test.WebApplication.Argon2id.Models;

namespace Test.WebApplication.Argon2id.Commands.PeopleCommand
{
    public class PeopleChangePasswordGetByIdCommandHandle : IRequestHandler<PeopleChangePasswordGetByIdCommand, bool>
    {
        public DatabaseContext Database { get; }
        public IArgon2idPasswordHasher Crypt { get; }

        public PeopleChangePasswordGetByIdCommandHandle(DatabaseContext database, IArgon2idPasswordHasher crypt)
        {
            Database = database;
            Crypt = crypt;
        }

        public async Task<bool> Handle(PeopleChangePasswordGetByIdCommand request, CancellationToken cancellationToken)
        {
            People people = await Database
                .People
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (Crypt.Valid(request.Password, people.PasswordSalt, people.PasswordHashed))
            {
                var hash = Crypt.Hash(request.NewPassord);
                people.PasswordHashed = hash.Hashed;
                people.PasswordSalt = hash.Salt;
                return (await Database.SaveChangesAsync() > 0);
            }
            return false;
        }
    }
}

