using MediatR;
using System.ComponentModel.DataAnnotations;
using Test.WebApplication.Models;

namespace Test.WebApplication.Commands.PeopleCommand
{
    public class PeopleCreateCommand : IRequest<People>
    {
        [Required()]
        public string Name { get; set; }

        [Required()]
        public string Password { get; set; }
    }
}
