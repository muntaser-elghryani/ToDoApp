using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.Dtos.TeamDtos;

namespace ToDoApp.BAL.Interfaces
{
    public interface ITeamService
    {
       
        Task<GetTeamDto> CreateTeam(CreateTeamDto createTeamDto);
        Task<List<GetTeamDto>> GetAllTeams();
    }
}
