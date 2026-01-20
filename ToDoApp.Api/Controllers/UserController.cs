using Microsoft.AspNetCore.Mvc;
using ToDoApp.BAL.Interfaces;
using ToDoApp.Dtos.UserDtos;


namespace ToDoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> CreatUser(CreateUserDto creatUserDto)
        {
                return Ok(await _userService.CreateUser(creatUserDto));
        }  

    }
}
