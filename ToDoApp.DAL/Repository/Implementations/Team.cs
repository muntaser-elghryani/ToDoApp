using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<bool> NameExists(string name)
        {
            return await _Context.Teams.AnyAsync(t => t.Name == name);
        }
    }
}
