using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Configuration
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Primary Key
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                   .ValueGeneratedNever();

            builder.Property(r => r.RoleName)
                   .IsRequired()
                   .HasMaxLength(50);

            // =========================
            // Seed Data (Default Roles)
            // =========================
            builder.HasData(
                    new Role { Id = 1, RoleName = "SuperAdmin" },
                    new Role { Id = 2, RoleName = "Manager" },
                    new Role { Id = 3, RoleName = "Employee" }
            );

        }
    }
}
