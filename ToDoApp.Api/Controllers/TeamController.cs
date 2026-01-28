using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoApp.BAL.Interfaces;
using ToDoApp.Dtos.TeamDtos;

namespace ToDoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _TeamService;

        public TeamController(ITeamService teamService)
        {
            _TeamService = teamService;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult<GetTeamDto>> CreateTeam([FromBody] CreateTeamDto createTeamDto)
        {

            
                
                if (string.IsNullOrEmpty(createTeamDto.Name)) 
                {
                    return BadRequest("Team Name Is Required");
                }
                    return Ok(await _TeamService.CreateTeam(createTeamDto));
          
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult<List<GetTeamDto>>> GetAllTeam()
        {
            var Result = await _TeamService.GetAllTeams();
            return Ok(Result);
        }
    }
}
