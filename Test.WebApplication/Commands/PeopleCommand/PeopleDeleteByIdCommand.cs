using MediatR;

namespace Test.WebApplication.Commands.PeopleCommand
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
