using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.BAL.Interfaces;
using ToDoApp.BAL.Jwt;
using ToDoApp.Dtos.AuthDto;

namespace ToDoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _User;
        private readonly IJwtService _JWt;

        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _User = userService;
            _JWt = jwtService; 
        }


        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDto logInDto)
        {
          
                var result = await _User.LogIn(logInDto);

                var token = _JWt.GenerateToken(result);

                Response.Cookies.Append("token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Path = "/",
                    Domain = "localhost",
                    Expires = DateTimeOffset.UtcNow.AddDays(1)
                });

                return Ok(result);
           
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/",
                Domain = "localhost",
            });

            return Ok();
        }

    }
}
