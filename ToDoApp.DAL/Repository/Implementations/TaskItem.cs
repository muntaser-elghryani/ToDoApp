using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Repository.Interfaces;
using ToDoApp.Dtos.TaskDtos;

namespace ToDoApp.DAL.Repository.Implementations
{
    public class TaskItem : ITaskItem
    {
        private readonly AppDbContext _Context;

        public TaskItem(AppDbContext Context)
        {
            _Context = Context;
        }
        public async Task<Entities.TaskItem> CreateTask(Entities.TaskItem task)
        {
           await _Context.TaskItems.AddAsync(task);
           await _Context.SaveChangesAsync();

            return task;
        }

        public async Task<List<TaskListDto>> GetAllTeamTaks(int TeamId)
        {
            return await _Context.TaskItems.AsNoTracking().Where(t => t.TeamId == TeamId).Select(task => new TaskListDto 
            {
                Id = task.Id,
                Title = task.Title,
                AssignedTo = task.AssignedTo.Name,
                Status = task.Status.ToString(),
                DueDate = task.DueDate,
            }).ToListAsync();
        }

    

        public async Task<Entities.TaskItem?> GetTaskById(int TaskId, int? TeamId = null, int? Enployee = null)
        {
            if (TeamId.HasValue)
                return await _Context.TaskItems.Where(t => t.Id == TaskId && t.TeamId == TeamId).FirstOrDefaultAsync();

            if(Enployee.HasValue)
                return await _Context.TaskItems.Where(t => t.Id == TaskId && t.AssignedToId == Enployee).FirstOrDefaultAsync();

            return null;
        }

        public async Task<List<Entities.TaskItem>> GetTasksByEmployeeId(int EmployeeId)
        {
            return await _Context.TaskItems.AsNoTracking().Where(t => t.AssignedToId == EmployeeId).ToListAsync();
        }
    }
}
