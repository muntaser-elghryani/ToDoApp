using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.DAL.Dtos
{
    public class GetUserRepoDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public int RoleId { get; set; } 
        public int? TeamId { get; set; }
        public int Status { get; set; } 
    }
}
