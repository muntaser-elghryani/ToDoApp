using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Dtos.AuthDto
{
    public class LogInDto
    {
        public string Phone {  get; set; } = string.Empty;
        public string Password {  get; set; } = string.Empty;
    }
}
