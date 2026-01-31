using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Dtos.TaskDtos
{
    public class TaskListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string AssignedTo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
    }
}
