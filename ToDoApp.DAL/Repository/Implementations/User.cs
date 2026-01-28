using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Dtos;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Enums;
using ToDoApp.DAL.Repository.Interface;

namespace ToDoApp.DAL.Repository.Implementations
{
    public class User : IUser
    {
        private readonly AppDbContext _Context;

        public User(AppDbContext appDbContext)
        {
            _Context = appDbContext;
        }

        public async Task<bool> PhoneExists(string Phone)
        {
            return await _Context.Users.AnyAsync(u => u.Phone == Phone);
        }

        public async Task<Entities.User> CreateUser(Entities.User user)
        {
            try
            {

                await _Context.Users.AddAsync(user);
                await _Context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                {
                    throw new Exception(ex.Message);
                }

            }
        }

        public async Task<Entities.User?> GetUserByPhone(string Phone)
        {
            return await _Context.Users.Where(t => t.Phone == Phone).FirstOrDefaultAsync();
        }

        public async Task<bool> GetUserById(int Id)
        {
            return await _Context.Users.AnyAsync(u =>u.Id == Id);
        }

        public async Task<int> DeleteUser(int Id)
        {
            return await _Context.Users.Where(u  => u.Id == Id && u.RoleId != (int)enUserRole.SuperAdmin).ExecuteDeleteAsync();
        }

        public IQueryable<Entities.User> GetAllUsers()
        {
                

            return _Context.Users.AsNoTracking(); ;
        }
    }
}
