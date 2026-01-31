using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Dtos.TaskDtos
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status {  get; set; } = string.Empty;
        public int AssignedToId { get; set; }
        public int TeamId { get; set; }
        public int CreatedById { get; set; }
        public int DueInDays { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
