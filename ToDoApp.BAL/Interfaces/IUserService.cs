using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.Dtos.AuthDto;
using ToDoApp.Dtos.UserDtos;

namespace ToDoApp.BAL.Interfaces
{
    public interface IUserService
    {
        public Task<GetUserDto> CreateUser(CreateUserDto createUserDto);
        public Task<LoginResponseDto> LogIn(LogInDto logInDto);
    }
}
