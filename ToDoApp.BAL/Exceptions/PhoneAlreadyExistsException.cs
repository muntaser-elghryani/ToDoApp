using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.BAL.Exceptions
{
    public class PhoneAlreadyExistsException : BusinessException
    {
        public PhoneAlreadyExistsException() : base("Phone number already exists.")
        {
        }
    }
}
