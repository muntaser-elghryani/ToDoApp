using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoApp.BAL.Interfaces;
using ToDoApp.Dtos.TaskDtos;

namespace ToDoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private ITaskItemService _TaskService;

        public TaskItemController(ITaskItemService taskItemService)
        {
            _TaskService = taskItemService;
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateTaskItem(CreateTaskDto createTaskDto)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int ManagerId))
                return Forbid();

            if (!int.TryParse(User.FindFirstValue("TeamId"), out int TeamId))
                return Forbid();

            return Ok(await _TaskService.CreateTaskItem(createTaskDto,ManagerId,TeamId));

        }


        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> GetTeamTasks()
        {

            if (!int.TryParse(User.FindFirstValue("TeamId"), out int TeamId))
                return Forbid();

            return Ok(await _TaskService.GetTeamTasks(TeamId));
        }



        [Authorize(Roles = "Manager,Employee")]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int Enployee))
                return Forbid();

            if (!int.TryParse(User.FindFirstValue("TeamId"), out int TeamId))
                return Forbid();

            var RoleName = User.FindFirstValue(ClaimTypes.Role.ToString());
            if (RoleName == null)
                return Forbid();


            return Ok(await _TaskService.GetTaskById(id, TeamId, Enployee, RoleName));

        }


        [Authorize(Roles = "Employee")]
        [HttpGet("me")]
        public async Task<IActionResult> GetTasksByEmployeeId()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int Enployee))
                return Forbid();

            return Ok(await _TaskService.GetTasksByEmployeeId(Enployee));
        }

    }
}
