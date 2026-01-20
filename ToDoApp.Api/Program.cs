using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDoApp.BAL.Implementations;
using ToDoApp.BAL.Interfaces;
using ToDoApp.DAL;
using ToDoApp.DAL.Repository.Implementations;
using ToDoApp.DAL.Repository.Interface;

namespace ToDoApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            //DI services DAL
            builder.Services.AddScoped<IUser, User>();


            //DI services BAL
            builder.Services.AddScoped<IUserService, UserService>();


            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
