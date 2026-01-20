using ToDoApp.BAL.Interfaces;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Enums;
using ToDoApp.DAL.Repository.Interface;
using ToDoApp.Dtos.UserDtos;

namespace ToDoApp.BAL.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUser _user;

        public UserService(IUser user)
        {
            _user = user;
        }
        public async Task<GetUserDto> CreateUser(CreateUserDto createUserDto)
        {

            var user = new User
            {
                Name = createUserDto.UserName,
                Phone = createUserDto.Phone,
                Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password),
                RoleId = createUserDto.RoleId,
                TeamId = createUserDto.TeamId,
                Status = enUserStatus.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var NewUser = await _user.CreateUser(user);

            return new GetUserDto
            {
                Id = NewUser.Id,
                Name = NewUser.Name,
                phone = NewUser.Phone,
                RoleName = Enum.GetName(typeof(enUserRole), NewUser.RoleId),
                TeamId = NewUser.TeamId,
                Status = NewUser.Status.ToString(),

                
            };

        }
    }
}
