using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Test.WebApplication.Argon2id.Models;

namespace Test.WebApplication.Argon2id.Commands.PeopleCommand
{
    public class PeopleListCommand: IRequest<IReadOnlyCollection<People>>
    {
    }
}

