using MediatR;
using Test.WebApplication.Models;

namespace Test.WebApplication.Commands.PeopleCommand
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
