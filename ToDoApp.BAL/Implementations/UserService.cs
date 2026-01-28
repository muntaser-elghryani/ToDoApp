using Microsoft.EntityFrameworkCore;
using ToDoApp.BAL.Interfaces;
using ToDoApp.BAL.Jwt;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Enums;
using ToDoApp.DAL.Repository.Implementations;
using ToDoApp.DAL.Repository.Interface;
using ToDoApp.DAL.Repository.Interfaces;
using ToDoApp.Dtos.AuthDto;
using ToDoApp.Dtos.UserDtos;
using User = ToDoApp.DAL.Entities.User;

namespace ToDoApp.BAL.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUser _user;
        private readonly IJwtService _Jwt;
        private readonly ITeam _team;

        public UserService(IUser user, IJwtService jwtService, ITeam team)
        {
            _user = user;
            _Jwt = jwtService;
            _team = team;
        }
        public async Task<GetUserDto> CreateUser(CreateUserDto createUserDto, string Role, int TeamId)
        {

            if (await _user.PhoneExists(createUserDto.Phone))
                throw new InvalidOperationException("Phone number already exists.");

            if (Role == enUserRole.Manager.ToString())
            {
                var Employee = new User
                {
                    Name = createUserDto.UserName,
                    Phone = createUserDto.Phone,
                    Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password),
                    RoleId = (int)enUserRole.Employee,
                    TeamId = TeamId,
                    Status = enUserStatus.Active,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                var NewEmployee = await _user.CreateUser(Employee);

                return new GetUserDto
                {
                    Id = NewEmployee.Id,
                    Name = NewEmployee.Name,
                    phone = NewEmployee.Phone,
                    RoleName = Enum.GetName(typeof(enUserRole), NewEmployee.RoleId),
                    TeamId = NewEmployee.TeamId,
                    Status = NewEmployee.Status.ToString(),
                };
            }
            else if (Role == enUserRole.SuperAdmin.ToString())
            {
                var Team = await _team.GetTeamById((int)createUserDto.TeamId);
                if (Team.ManagerId != null)
                {
                    throw new InvalidOperationException("this team already  manager  exists");
                }


                var Manager = new User
                {
                    Name = createUserDto.UserName,
                    Phone = createUserDto.Phone,
                    Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password),
                    RoleId = (int)enUserRole.Manager,
                    TeamId = createUserDto.TeamId,
                    Status = enUserStatus.Active,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };
                var newManager = await _user.CreateUser(Manager);

                await _team.AddManager(Team, newManager.Id);

                return new GetUserDto
                {
                    Id = newManager.Id,
                    Name = newManager.Name,
                    phone = newManager.Phone,
                    RoleName = Enum.GetName(typeof(enUserRole), newManager.RoleId),
                    TeamId = newManager.TeamId,
                    Status = newManager.Status.ToString(),
                };
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

        }




        public async Task<bool> DeleteUser(int Id)
        {
            bool affectedRows = await _user.DeleteUser(Id) > 0;

            if (!affectedRows)
                throw new KeyNotFoundException("This User Not Found Or Admin");

            return true;
        }

        public async Task<List<GetUserDto>> GetAllUsers()
        {
            var users = await _user.GetAllUsers().ToListAsync();



            return users.Select(u => new GetUserDto
            {
                Id = u.Id,
                Name = u.Name,
                phone = u.Phone,
                RoleName = Enum.GetName(typeof(enUserRole), u.RoleId),
                TeamId = u.TeamId,
                Status = u.Status.ToString(),

            }).ToList();
        }

        public async Task<GetUserDto> GetUserByPhone(string Phone)
        {
            var user = await _user.GetUserByPhone(Phone);

            if (user == null)
            {
                throw new KeyNotFoundException($"this  {Phone} not found");
            }

            return new GetUserDto
            {
                Id = user.Id,
                Name = user.Name,
                phone = user.Phone,
                RoleName = Enum.GetName(typeof(enUserRole), user.RoleId),
                TeamId = user.TeamId,
                Status = user.Status.ToString(),
            };
        }

        public async Task<LoginResponseDto> LogIn(LogInDto logInDto)
        {


            var user = await _user.GetUserByPhone(logInDto.Phone);

            if (user == null)
                throw new InvalidOperationException("Phone not found");

            if (!BCrypt.Net.BCrypt.Verify(logInDto.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid password");


            return new LoginResponseDto
            {
                Id = user.Id,
                Username = user.Name,
                Phone = user.Phone,
                RoleName = Enum.GetName(typeof(enUserRole), user.RoleId),
                TeamId = user.TeamId,
            };

        }
    }
}
