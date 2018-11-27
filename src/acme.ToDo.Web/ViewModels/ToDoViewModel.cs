using acme.ToDo.Models;
using System.Collections.Generic;

namespace acme.ToDo.Web.ViewModels
{
    public class ToDoViewModel
    {
        public ToDoViewModel()
        {
            Items = new List<ToDoItem>();
        }

        public List<ToDoItem> Items { get; set; }
    }
}
