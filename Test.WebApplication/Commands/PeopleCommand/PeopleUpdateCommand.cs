using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Test.WebApplication.Commands.PeopleCommand
{
    public class PeopleUpdateCommand : IRequest<bool>
    {
        public PeopleUpdateCommand()
        {
        }

        [Required()]
        public int Id { get; set; }

        [Required()]
        public string Name { get; set; }

        public string Password { get; set; }
    }
}
