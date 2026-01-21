using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Dtos.TeamDtos
{
    public class CreateTeamDto
    {
        public string Name { get; set; } = string.Empty;
        public int? ManagerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
