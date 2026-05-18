using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.BAL.Exceptions
{
    public class TeamNameAlreadyExistsException : BusinessException
    {
        public TeamNameAlreadyExistsException() : base("Team Name already exists")
        {
        }
    }
}
