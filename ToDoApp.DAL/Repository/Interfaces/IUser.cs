

using ToDoApp.DAL.Dtos;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Repository.Interface
{
    public interface IUser
    {
        Task<User> CreateUser(User user); 
        Task<bool> PhoneExists(string phone);
    }
}
