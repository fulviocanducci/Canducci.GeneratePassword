using MediatR;
using Test.WebApplication.Argon2id.Models;

namespace Test.WebApplication.Argon2id.Commands.PeopleCommand
{
    public class PeopleGetByIdCommand: IRequest<People>
    {
        public PeopleGetByIdCommand(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }
}

