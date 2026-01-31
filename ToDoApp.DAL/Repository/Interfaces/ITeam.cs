using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Repository.Interfaces
{
    public interface ITeam
    {
        Task<Team> CreateTeam(Team team);
        Task<bool> NameExists(string name);
        Task<List<Team>> GetAllTeams();
        Task<bool> TeamHasManager(int id);
        Task<Team?> GetTeamById(int id);
        Task<bool> AddManager(Team team, int ManagerId);

        Task<Team?> GetTeamByName(string name);
    }
}
