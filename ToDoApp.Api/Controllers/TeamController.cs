using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<ActionResult<GetTeamDto>> CreateTeam([FromBody] CreateTeamDto createTeamDto)
        {
            try
            {
                if (string.IsNullOrEmpty(createTeamDto.Name)) 
                {
                    return BadRequest("Team Name Is Required");
                }
                return Ok(await _TeamService.CreateTeam(createTeamDto));
            }
            catch
            {
                return BadRequest("Team Name already exists.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<GetTeamDto>>> GetAllTeam()
        {
            var Result = await _TeamService.GetAllTeams();
            return Ok(Result);
        }
    }
}
