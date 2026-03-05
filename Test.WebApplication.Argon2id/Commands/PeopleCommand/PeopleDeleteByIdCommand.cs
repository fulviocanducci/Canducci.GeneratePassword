using MediatR;

namespace Test.WebApplication.Argon2id.Commands.PeopleCommand
{
    public class PeopleDeleteByIdCommand : IRequest<bool>
    {
        public PeopleDeleteByIdCommand(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }
}

