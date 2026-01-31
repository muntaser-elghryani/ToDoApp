using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.Dtos.TaskDtos;

namespace ToDoApp.BAL.Interfaces
{
    public interface ITaskItemService
    {
        Task<GetMyTaskDto> CreateTaskItem(CreateTaskDto createTaskDto,int CreatedById,int TeamId);
        Task<List<TaskListDto>> GetTeamTasks(int TeamId);

        Task<TaskDetailsDto> GetTaskById(int TaskId, int TeamId, int Enployee, string RoleName);
        Task<List<GetMyTaskDto>> GetTasksByEmployeeId(int EmployeeId);
        
    }
}
