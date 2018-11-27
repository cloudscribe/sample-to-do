using acme.ToDo.Models;
using acme.ToDo.Web.Services;
using acme.ToDo.Web.ViewModels;
using cloudscribe.Web.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace acme.ToDo.Web.Controllers
{
    public class ToDoController : Controller
    {
        public ToDoController(ToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        private readonly ToDoService _toDoService;

        [Authorize(Policy = "ToDoPolicy")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var items = await _toDoService.GetIncompleteItems(cancellationToken);

            var model = new ToDoViewModel()
            {
                Items = items
            };

            return View(model);
        }

        [Authorize(Policy = "ToDoPolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(ToDoItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            
            await _toDoService.AddItem(newItem);
            this.AlertSuccess("To Do item added", true);

            return RedirectToAction("Index");
        }

        [Authorize(Policy = "ToDoPolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }
            
            await _toDoService.MarkAsDone(id);
            this.AlertSuccess("To Do item marked as completed", true);
            
            return RedirectToAction("Index");
        }

    }
}
