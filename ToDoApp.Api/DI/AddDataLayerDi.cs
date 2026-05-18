using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Repository.Implementations;
using ToDoApp.DAL.Repository.Interface;
using ToDoApp.DAL.Repository.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class AddDataLayerDi
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services)
    {
        services.AddScoped<IUser, UserRepo>();
        services.AddScoped<ITeam, TeamRepo>();
        services.AddScoped<ITaskItem, TaskItemRepo>();

        return services;
        
    }
}
