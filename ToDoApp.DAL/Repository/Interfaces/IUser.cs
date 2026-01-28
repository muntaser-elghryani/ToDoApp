

using ToDoApp.DAL.Dtos;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Repository.Interface
{
    public interface IUser
    {
        Task<User> CreateUser(User user); 
        Task<bool> PhoneExists(string phone);
        Task<Entities.User?> GetUserByPhone(string Phone);
        Task<bool> GetUserById(int Id);
        Task<int> DeleteUser(int Id);
        IQueryable<User> GetAllUsers();

    }
}
