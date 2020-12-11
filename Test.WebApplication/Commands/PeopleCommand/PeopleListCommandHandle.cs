using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Test.WebApplication.Models;

namespace Test.WebApplication.Commands.PeopleCommand
{
    public class PeopleListCommandHandle : IRequestHandler<PeopleListCommand, IReadOnlyCollection<People>>
    {
        public DatabaseContext Database { get; }

        public PeopleListCommandHandle(DatabaseContext database)
        {
            Database = database;            
        }
        public async Task<IReadOnlyCollection<People>> Handle(PeopleListCommand request, CancellationToken cancellationToken)
        {
            return await Database.People.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
