

using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Repository.Interface
{
    public interface IUser
    {
        Task<User> CreateUser(User user); 
        Task<bool> PhoneExists(string phone);
        Task<bool> EmployeeExists(int EmployeeId);
        Task<User?> GetEmployeeById(int EmployeeId);
        Task<Entities.User?> GetUserByPhone(string Phone);
        Task<int> DeleteUser(int Id);
        IQueryable<User> GetAllUsers();
        Task<List<User>> GetAllManager();
        Task<List<User>> GetAllEmployeeTeamScoped(int TeamId);

    }
}
