
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.DAL.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public List<User> Users { get; set; } = null!;
    }
}
