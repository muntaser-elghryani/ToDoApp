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


  
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("CreatManager")]
        public async Task<ActionResult> CreateManager(CreateUserDto creatUserDto)
        {
            return Ok(await _userService.CreateManager(creatUserDto));
           
        }


        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteUser(int Id)
        {
            await _userService.DeleteUser(Id);
            return Ok();
        }


        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {

            return Ok(await _userService.GetAllUsers());

        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("phone")]
        public async Task<IActionResult> GetUserByPhone([FromQuery] string Phone)
        {

            return Ok(await _userService.GetUserByPhone(Phone));

        }


        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("Managers")]
        public async Task<IActionResult> GetAllManagers()
        {
            return Ok(await _userService.GetAllManagers());

        }



        //manager 

        [Authorize(Roles = "Manager")]
        [HttpGet("Employee")]
        public async Task<IActionResult> GetAllEmployeeTeamScoped()
        {


            if (!int.TryParse(User.FindFirstValue("TeamId"), out int TeamId))
                return Forbid();


            return Ok(await _userService.GetAllEmployeeTeamScoped(TeamId));
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("CreatEmployee")]
        public async Task<ActionResult> CreateEmployee(CreateUserDto creatUserDto)
        {
            if (!int.TryParse(User.FindFirstValue("TeamId"), out int TeamId))
            {
                return Forbid();
            }

            return Ok(await _userService.CreateEmpolyee(creatUserDto, TeamId));
        }


    }
}
