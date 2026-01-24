using ToDoApp.BAL.Interfaces;
using ToDoApp.BAL.Jwt;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Enums;
using ToDoApp.DAL.Repository.Interface;
using ToDoApp.Dtos.AuthDto;
using ToDoApp.Dtos.UserDtos;

namespace ToDoApp.BAL.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUser _user;
        private readonly IJwtService _Jwt;

        public UserService(IUser user, IJwtService jwtService)
        {
            _user = user;
            _Jwt = jwtService;
        }
        public async Task<GetUserDto> CreateUser(CreateUserDto createUserDto)
        {

            if (await _user.PhoneExists(createUserDto.Phone))
            {
                throw new InvalidOperationException("Phone number already exists.");
            }

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

        public async Task<LoginResponseDto> LogIn(LogInDto logInDto)
        {


            var user = await _user.GetUserByPhone(logInDto.Phone);

            if (user == null)
                throw new InvalidOperationException("Phone not found");

            if (!BCrypt.Net.BCrypt.Verify(logInDto.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid password");


            var result = new LoginResponseDto
            {
                Id = user.Id,
                Username = user.Name,
                Phone = user.Phone,
                RoleName = Enum.GetName(typeof(enUserRole),user.RoleId),
                TeamId= user.TeamId,

            };




            return result;
              
            
        }
    }
}
