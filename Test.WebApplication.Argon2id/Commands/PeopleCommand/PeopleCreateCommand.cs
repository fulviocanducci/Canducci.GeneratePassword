using MediatR;
using System.ComponentModel.DataAnnotations;
using Test.WebApplication.Argon2id.Models;

namespace Test.WebApplication.Argon2id.Commands.PeopleCommand
{
    public class PeopleCreateCommand : IRequest<People>
    {
        [Required(ErrorMessage = "Digite o nome")]
        [MinLength(3, ErrorMessage = "Digite no minimo 3 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Digite a senha")]
        [MinLength(5, ErrorMessage ="Digite no minimo 5 caracteres")]
        public string Password { get; set; }
    }
}

