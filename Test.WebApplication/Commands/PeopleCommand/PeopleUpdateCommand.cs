using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Test.WebApplication.Commands.PeopleCommand
{
    public class PeopleUpdateCommand : IRequest<bool>
    {

        [Required()]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome")]
        [MinLength(3, ErrorMessage = "Digite no minimo 3 caracteres")]
        public string Name { get; set; }

        [MinLength(5, ErrorMessage = "Digite no minimo 5 caracteres")]
        public string Password { get; set; }
    }
}
