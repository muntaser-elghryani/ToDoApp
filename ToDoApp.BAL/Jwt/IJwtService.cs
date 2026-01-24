using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.Dtos.AuthDto;

namespace ToDoApp.BAL.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(LoginResponseDto loginResponseDto);

    }
}
