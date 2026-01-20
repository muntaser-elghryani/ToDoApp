using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // هذا سيبحث تلقائيًا عن كل كلاس يطبق IEntityTypeConfiguration داخل نفس Assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
