using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.BAL.Interfaces;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Repository.Interfaces;
using ToDoApp.Dtos.TeamDtos;

namespace ToDoApp.BAL.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly ITeam _Team;

        public TeamService(ITeam team)
        {
            _Team = team;
        }
        public async Task<GetTeamDto> CreateTeam(CreateTeamDto createTeamDto)
        {
            if (await _Team.NameExists(createTeamDto.Name))
            {
                throw new Exception("Team Name already exists.");
            }

            Team team = new Team 
            {
                Name = createTeamDto.Name,
                ManagerId = createTeamDto.ManagerId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var NewTeam = await _Team.CreateTeam(team);

            return new GetTeamDto
            {
                Id = team.Id,
                Name = team.Name,
                ManagerId = team.ManagerId,
                CreatedAt = team.CreatedAt.ToString("yyyy-mm-dd HH:MM"),
                UpdatedAt = team.UpdatedAt.ToString("yyyy-mm-dd HH:MM"),
            };

        }

        public async Task<List<GetTeamDto>> GetAllTeams()
        {
            var teams = await _Team.GetAllTeams();


            var Result = teams.Select(t => new GetTeamDto
            {
                Id = t.Id,
                Name = t.Name,
                ManagerId = t.ManagerId,
                CreatedAt = t.CreatedAt.ToString("yyyy-mm-dd HH:MM"),
                UpdatedAt = t.UpdatedAt.ToString("yyyy-mm-dd HH:MM"),
            }).ToList();
            
            return Result;
            
        }
    }
}
