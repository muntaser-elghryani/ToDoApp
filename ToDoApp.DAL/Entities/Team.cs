using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.DAL.Entities
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int ManagerId { get; set; }

        public List<User> Users { get; set; } = new();
        public List<TaskItem> Tasks { get; set; } = new();


        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
