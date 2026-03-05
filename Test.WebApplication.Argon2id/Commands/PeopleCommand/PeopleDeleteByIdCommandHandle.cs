using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Test.WebApplication.Argon2id.Models;

namespace Test.WebApplication.Argon2id.Commands.PeopleCommand
{
    public class PeopleDeleteByIdCommandHandle : IRequestHandler<PeopleDeleteByIdCommand, bool>
    {
        public DatabaseContext Database { get; }

        public PeopleDeleteByIdCommandHandle(DatabaseContext database)
        {
            Database = database;
        }


        public async Task<bool> Handle(PeopleDeleteByIdCommand request, CancellationToken cancellationToken)
        {
            People people = Database.People.Find(request.Id);
            if (people != null)
            {
                Database.People.Remove(people);
                return (await Database.SaveChangesAsync(cancellationToken) > 0);
            }
            return false;
        }
    }
}

