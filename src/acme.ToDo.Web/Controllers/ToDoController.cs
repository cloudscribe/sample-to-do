using acme.ToDo.Web.Services;
using acme.ToDo.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using cloudscribe.Web.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using acme.ToDo.Models;

namespace acme.ToDo.Web.Controllers
{
    public class ToDoController : Controller
    {
        public ToDoController(ToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        private readonly ToDoService _toDoService;

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var items = await _toDoService.GetIncompleteItems(cancellationToken);

            var model = new ToDoViewModel()
            {
                Items = items
            };

            return View(model);
        }

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
