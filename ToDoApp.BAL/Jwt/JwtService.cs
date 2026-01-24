using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoApp.Dtos.AuthDto;

namespace ToDoApp.BAL.Jwt
{
    public class JwtService : IJwtService
    {
        private IConfiguration _config;

        public JwtService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string GenerateToken(LoginResponseDto loginResponseDto)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loginResponseDto.Id.ToString()),
                new Claim(ClaimTypes.Name , loginResponseDto.Username.ToString()),
                new Claim(ClaimTypes.MobilePhone , loginResponseDto.Phone.ToString()),
                new Claim(ClaimTypes.Role , loginResponseDto.RoleName.ToString()),
                new Claim("TeamId", loginResponseDto.TeamId.ToString())
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(2),
                    signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
