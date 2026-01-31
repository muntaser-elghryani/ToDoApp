using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Enums;

namespace ToDoApp.DAL.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public enTaskStatus Status { get; set; }

        public int AssignedToId { get; set; }
        public User AssignedTo { get; set; } = null!;

        public int TeamId { get; set; }
        public Team Team { get; set; } = null!; 

        public int CreatedById { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
