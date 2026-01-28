using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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


        [Authorize(Roles = "SuperAdmin,Manager")]
        [HttpPost]
       public async Task<ActionResult> CreatUser(CreateUserDto creatUserDto)
        {
            var Role = User.FindFirst(ClaimTypes.Role).Value;

            int TeamId = Convert.ToInt32(User.FindFirst("TeamId").Value);

            return Ok(await _userService.CreateUser(creatUserDto,Role.ToString(), TeamId));
        }

        
        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteUser(int Id) 
        {
            await _userService.DeleteUser(Id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            
                return Ok(await _userService.GetAllUsers());

        }


        [HttpGet("phone")]
        public async Task<IActionResult> GetUserByPhone([FromQuery]string Phone) 
        {
                
                return Ok(await _userService.GetUserByPhone(Phone));
            
        }

    

    }
}
