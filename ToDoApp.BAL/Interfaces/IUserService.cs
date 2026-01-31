using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.Dtos.AuthDto;
using ToDoApp.Dtos.UserDtos;

namespace ToDoApp.BAL.Interfaces
{
    public interface IUserService
    {
        public Task<GetUserDto> CreateEmpolyee(CreateUserDto createUserDto,int TeamId);
        public Task<GetUserDto> CreateManager(CreateUserDto createUserDto);

        public Task<LoginResponseDto> LogIn(LogInDto logInDto);
        Task<bool> DeleteUser(int  Id);
        Task<GetUserDto> GetUserByPhone(string Phone);
        Task<List<GetUserDto>> GetAllManagers();
        Task<List<GetUserDto>> GetAllEmployeeTeamScoped(int TeamId);
        Task<List<GetUserDto>> GetAllUsers();
    }
}
