using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Enums;

namespace ToDoApp.DAL.Entities
{
    public class User
    {
        // Primary Key
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public int? TeamId { get; set; }

        public Role Role { get; set; } = null!;

        public Team? Team { get; set; }
        public List<TaskItem> TaskItems { get; set; } = new();

        public enUserStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
