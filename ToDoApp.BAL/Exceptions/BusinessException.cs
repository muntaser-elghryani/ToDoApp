using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.BAL.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) 
            :base(message) 
        {
        }
    }
}
