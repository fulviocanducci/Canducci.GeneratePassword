using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Test.WebApplication.Models;

namespace Test.WebApplication.Commands.PeopleCommand
{
    public class PeopleListCommand: IRequest<IReadOnlyCollection<People>>
    {
    }
}
