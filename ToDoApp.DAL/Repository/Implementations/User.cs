using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Dtos;
using ToDoApp.DAL.Entities;
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
       

        public async Task<Entities.User> CreateUser(Entities.User user)
        {
            await _Context.Users.AddAsync(user);
            await _Context.SaveChangesAsync();

            return user;
        }
    }
}
