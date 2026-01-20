using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Dtos.UserDtos
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string? RoleName { get; set; } = string.Empty;
        public int? TeamId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
