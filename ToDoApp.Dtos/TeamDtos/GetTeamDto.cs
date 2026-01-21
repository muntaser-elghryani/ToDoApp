using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Dtos.TeamDtos
{
    public class GetTeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ManagerId {  get; set; }
        public String CreatedAt { get; set; } = string.Empty;
        public String UpdatedAt { get; set; } = string.Empty;
    }
}
