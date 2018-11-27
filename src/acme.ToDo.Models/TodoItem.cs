using System;
using System.Collections.Generic;
using System.Text;

namespace acme.ToDo.Models
{
    public class ToDoItem
    {
        public ToDoItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public Guid UserId { get; set; }
        public Guid SiteId { get; set; }
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    }
}
