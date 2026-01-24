using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Dtos.AuthDto
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Phone {  get; set; } = string.Empty;
        public string RoleName{  get; set; } = string.Empty;
        public int? TeamId { get; set; }

    }
}
