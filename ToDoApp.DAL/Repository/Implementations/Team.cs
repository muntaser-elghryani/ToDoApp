using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Enums;
using ToDoApp.DAL.Repository.Interfaces;

namespace ToDoApp.DAL.Repository.Implementations
{
    public class Team : ITeam
    {
        private AppDbContext _Context;

        public Team(AppDbContext appDbContext)
        {
            _Context = appDbContext;
        }

        public async Task<bool> AddManager(Entities.Team team,int ManagerId)
        {
            var AddManager = team;
            AddManager.ManagerId = ManagerId;
            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<Entities.Team> CreateTeam(Entities.Team team)
        {
            try
            {
                await _Context.Teams.AddAsync(team);
                await _Context.SaveChangesAsync();
                return team;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);

            }
           
        }

        public async Task<List<Entities.Team>> GetAllTeams()
        {
            return await _Context.Teams.ToListAsync();
        }

        public async Task<Entities.Team?> GetTeamById(int id)
        {
            return await _Context.Teams.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> NameExists(string name)
        {
            return await _Context.Teams.AnyAsync(t => t.Name == name);
        }

        public async Task<bool> TeamHasManager(int id)
        {
            return await _Context.Teams.AnyAsync(t => t.Id == id && t.ManagerId != null);

        }
    }
}
