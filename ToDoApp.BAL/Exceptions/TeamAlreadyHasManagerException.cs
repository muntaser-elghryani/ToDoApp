

namespace ToDoApp.BAL.Exceptions
{
    public class TeamAlreadyHasManagerException : BusinessException
    {
        public TeamAlreadyHasManagerException()
            : base("This team already has a manager")
        {
        }
    }
}
