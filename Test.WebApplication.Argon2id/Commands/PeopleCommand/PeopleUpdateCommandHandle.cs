using Canducci.GeneratePassword.Argon2id;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Test.WebApplication.Argon2id.Models;

namespace Test.WebApplication.Argon2id.Commands.PeopleCommand
{
    public class PeopleUpdateCommandHandle : IRequestHandler<PeopleUpdateCommand, bool>
    {
        public DatabaseContext Database { get; }
        public IArgon2idPasswordHasher Crypt { get; }

        public PeopleUpdateCommandHandle(DatabaseContext database, IArgon2idPasswordHasher crypt)
        {
            Database = database;
            Crypt = crypt;
        }

        public async Task<bool> Handle(PeopleUpdateCommand request, CancellationToken cancellationToken)
        {
            People people = Database.People.Find(request.Id);

            if (people != null)
            {
                people.Name = request.Name;
                if (!string.IsNullOrEmpty(request.Password))
                {
                    var hash = Crypt.Hash(request.Password);
                    people.PasswordHashed = hash.Hashed;
                    people.PasswordSalt = hash.Salt;
                }
                return (await Database.SaveChangesAsync(cancellationToken) > 0);
            }
            return false;
        }
    }
}

