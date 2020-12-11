using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Test.WebApplication.Commands.PeopleCommand;
using Test.WebApplication.Models;

namespace Test.WebApplication.Controllers
{
    public class PeopleController : Controller
    {
        public IMediator Mediator { get; }

        public PeopleController(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            return View(await Mediator.Send(new PeopleListCommand()));
        }

        [HttpGet()]
        public async Task<IActionResult> ChangePassword(int id)
        {
            TempData.TryGetValue("ChangePassword", out object value);
            ViewData["ChangePassword"] = value;

            People people = await Mediator.Send(new PeopleGetByIdCommand(id));
            PeopleChangePasswordGetByIdCommand command =
                new PeopleChangePasswordGetByIdCommand()
                {
                    Id = people.Id,
                    Name = people.Name,
                    Password = string.Empty,
                    NewPassord = string.Empty
                };
            
            return View(command);
        }

        [HttpPost()]
        public async Task<IActionResult> ChangePassword(PeopleChangePasswordGetByIdCommand command)
        {
            if (ModelState.IsValid)
            {
                TempData["ChangePassword"] = await Mediator.Send(command);
            }
            
            return RedirectToAction("ChangePassword", new { command.Id });
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await Mediator.Send(new PeopleGetByIdCommand(id.Value));
            if (people != null)
            {
                return View(people);
            }

            return NotFound();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PeopleCreateCommand command)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }

            return View(command);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await Mediator.Send(new PeopleGetByIdCommand(id.Value));
            if (people == null)
            {
                return NotFound();
            }

            return View(new PeopleUpdateCommand
            {
                Id = people.Id,
                Name = people.Name
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PeopleUpdateCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Mediator.Send(command);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await Mediator.Send(new PeopleGetByIdCommand(id.Value));
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Mediator.Send(new PeopleDeleteByIdCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
