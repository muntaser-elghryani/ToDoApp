using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Entities;
using ToDoApp.Dtos.TaskDtos;

namespace ToDoApp.DAL.Repository.Interfaces
{
    public interface ITaskItem
    {
        Task<TaskItem> CreateTask(TaskItem task);
        Task<List<TaskListDto>> GetAllTeamTaks(int TeamId);
        Task<TaskItem?> GetTaskById(int TaskId, int? TeamId = null, int? Enployee = null);
        Task<List<TaskItem>> GetTasksByEmployeeId(int EmployeeId);
    }
}
