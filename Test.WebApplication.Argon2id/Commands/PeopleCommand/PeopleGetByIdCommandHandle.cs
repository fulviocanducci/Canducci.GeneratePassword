using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Test.WebApplication.Argon2id.Models;

namespace Test.WebApplication.Argon2id.Commands.PeopleCommand
{
    public class PeopleGetByIdCommandHandle : IRequestHandler<PeopleGetByIdCommand, People>
    {
        public DatabaseContext Database { get; }

        public PeopleGetByIdCommandHandle(DatabaseContext database)
        {
            Database = database;
        }
        public async Task<People> Handle(PeopleGetByIdCommand request, CancellationToken cancellationToken)
        {
            return await Database.People.FindAsync(new object[] { request.Id }, cancellationToken);
        }
    }
}

