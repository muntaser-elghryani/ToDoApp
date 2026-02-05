using Microsoft.Extensions.DependencyInjection;
using ToDoApp.DAL.Entities;


namespace ToDoApp.DAL.DataSeed
{
    public static class DataSeed
    {
        public static async Task SeedSuperAdmin(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (!db.Users.Any(x => x.RoleId == 1))
            {
                var hash = BCrypt.Net.BCrypt.HashPassword("56246204");
                db.Users.Add(new User
                {
                    Name = "muntaser",
                    Phone = "0911781787",
                    Password = hash,
                    RoleId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
                );
                await db.SaveChangesAsync();
            }
        }
    }
}
