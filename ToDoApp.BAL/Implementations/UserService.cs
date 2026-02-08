using Microsoft.EntityFrameworkCore;
using ToDoApp.BAL.Exceptions;
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

        public async Task<GetUserDto> CreateEmpolyee(CreateUserDto createUserDto, int TeamId)
        {

            if (await _user.PhoneExists(createUserDto.Phone))
                throw new InvalidOperationException("Phone number already exists.");


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

        public async Task<GetUserDto> CreateManager(CreateUserDto createUserDto)
        {


            if (await _user.PhoneExists(createUserDto.Phone))
                throw new PhoneAlreadyExistsException();

            if(createUserDto.TeamId == null)
                throw new ArgumentNullException("team id is null");

            

            var Taem = await _team.GetTeamById(createUserDto.TeamId.Value);

            if (Taem == null || Taem.ManagerId != null)
            {
                throw new TeamAlreadyHasManagerException();
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
            var NewManager = await _user.CreateUser(Manager);

            if(await _team.AddManager(Taem, NewManager.Id))
            {
                return new GetUserDto
                {
                    Id = NewManager.Id,
                    Name = NewManager.Name,
                    phone = NewManager.Phone,
                    RoleName = Enum.GetName(typeof(enUserRole), NewManager.RoleId),
                    TeamId = NewManager.TeamId,
                    Status = NewManager.Status.ToString(),
                };
            }
            return new GetUserDto();


        }

        public async Task<bool> DeleteUser(int Id)
        {
            bool affectedRows = await _user.DeleteUser(Id) > 0;

            if (!affectedRows)
                throw new KeyNotFoundException("This User Not Found Or Admin");

            return true;
        }

        public async Task<List<GetUserDto>> GetAllEmployeeTeamScoped(int TeamId)
        {
            var Employee = await _user.GetAllEmployeeTeamScoped(TeamId);
            if (Employee == null)
                throw new KeyNotFoundException("no Employee");

            return Employee.Select(e => new GetUserDto 
            {
                Id = e.Id,
                Name = e.Name,
                phone = e.Phone,
                RoleName = Enum.GetName(typeof (enUserRole), e.RoleId),
                TeamId = e.TeamId,
                Status = e.Status.ToString(),
            }).ToList();
        }

        public async Task<List<GetUserDto>> GetAllManagers()
        {
           var Managers = await _user.GetAllManager();
            if (Managers == null)
            {
                throw new KeyNotFoundException("No Managers");
            }
            return Managers.Select(m => new GetUserDto
            {
                Id = m.Id,
                Name = m.Name,
                phone = m.Phone,
                RoleName = Enum.GetName(typeof (enUserRole), m.RoleId),
                TeamId = m.TeamId,
                Status= m.Status.ToString(),

            }).ToList();
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
