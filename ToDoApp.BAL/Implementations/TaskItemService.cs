using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.BAL.Interfaces;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Enums;
using ToDoApp.DAL.Repository.Interface;
using ToDoApp.DAL.Repository.Interfaces;
using ToDoApp.Dtos.TaskDtos;

namespace ToDoApp.BAL.Implementations
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItem _TaskItem;
        private readonly IUser _User;

        public TaskItemService(ITaskItem taskItem,IUser user)
        {
            _TaskItem = taskItem;
            _User = user;
        }

        public async Task<GetMyTaskDto> CreateTaskItem(CreateTaskDto createTaskDto, int CreatedById, int TeamId)
        {

            var employee = await _User.GetEmployeeById(createTaskDto.AssignedToId)
                ?? throw new KeyNotFoundException("Employee not found.");

            if (employee.TeamId != TeamId)
                throw new UnauthorizedAccessException("Employee not in your team");


            var taskItem = new TaskItem
            {
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                Status = enTaskStatus.Pending,
                AssignedToId = createTaskDto.AssignedToId,
                TeamId = TeamId,
                CreatedById = CreatedById,
                DueDate = DateTime.UtcNow.AddDays(createTaskDto.DueInDays),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

           

            var newTaskItem = await _TaskItem.CreateTask(taskItem);

            return new GetMyTaskDto
            {
                Id = newTaskItem.Id,
                Title = newTaskItem.Title,
                Description = newTaskItem.Description,
                Status = newTaskItem.Status.ToString(),
                DueDate = newTaskItem.DueDate,
            };
            
        }

        public async Task<TaskDetailsDto> GetTaskById(int TaskId, int TeamId, int Enployee, string RoleName)
        {
            if (RoleName == enUserRole.Manager.ToString())
            {
                var TaskItem = await _TaskItem.GetTaskById(TaskId, TeamId,null);

                if (TaskItem == null)
                    throw new KeyNotFoundException("Task Not Found");

                return new TaskDetailsDto
                {
                    Id = TaskItem.Id,
                    Title = TaskItem.Title,
                    Description = TaskItem.Description,
                    Status = TaskItem.Status.ToString(),
                    AssignedToId = TaskItem.AssignedToId,
                    CreatedById = TaskItem.CreatedById,
                    DueDate = TaskItem.DueDate,
                    CreatedAt = TaskItem.CreatedAt,
                    UpdatedAt = TaskItem.UpdatedAt,
                };
            }
            else if (RoleName == enUserRole.Employee.ToString())
            {
                var TaskItem = await _TaskItem.GetTaskById(TaskId, null, Enployee);
                if (TaskItem == null)
                throw new KeyNotFoundException("Task Not Found");

                return new TaskDetailsDto
                {
                    Id = TaskItem.Id,
                    Title = TaskItem.Title,
                    Description = TaskItem.Description,
                    Status = TaskItem.Status.ToString(),
                    AssignedToId = TaskItem.AssignedToId,
                    CreatedById = TaskItem.CreatedById,
                    DueDate = TaskItem.DueDate,
                    CreatedAt = TaskItem.CreatedAt,
                    UpdatedAt = TaskItem.UpdatedAt,
                };
            }
            else 
            {
                return new TaskDetailsDto();
            }
        }

        public async Task<List<GetMyTaskDto>> GetTasksByEmployeeId(int EmployeeId)
        {
            var Tasks = await _TaskItem.GetTasksByEmployeeId(EmployeeId);
            if (Tasks.Count == 0 )
                return new List<GetMyTaskDto>();

            return Tasks.Select(t => new GetMyTaskDto
            {
                Id=t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate=t.DueDate,
                Status = t.Status.ToString(),
            }).ToList();

        }

        public async Task<List<TaskListDto>> GetTeamTasks(int TeamId)
        {
           var Tasks = await _TaskItem.GetAllTeamTaks(TeamId);

            if (Tasks.Count == 0)
                return new List<TaskListDto>();

            return Tasks;


            
        }
    }
}
