using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Test.WebApplication.Argon2id.Commands.PeopleCommand
{
    public class PeopleChangePasswordGetByIdCommand : IRequest<bool>
    {

        [Required()]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required()]
        public string Password { get; set; }
        [Required()]
        public string NewPassord { get; set; }
    }
}

