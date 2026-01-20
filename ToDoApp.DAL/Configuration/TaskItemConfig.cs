using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Configuration
{
    public class TaskItemConfig : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(t => t.Description)
                   .HasMaxLength(1000);

            builder.Property(t => t.Status)
                   .HasColumnType("varchar(50)")
                   .IsRequired();

            builder.Property(t => t.DueDate)
                   .IsRequired();

            builder.Property(t => t.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.Property(t => t.UpdatedAt)
                   .IsRequired();

            // ====================================
            // Relationships
            // ====================================

            // AssignedTo: Many Tasks → One User
            builder.HasOne(t => t.AssignedTo)
                   .WithMany(u => u.TaskItems)
                   .HasForeignKey(t => t.AssignedToId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Team: Many Tasks → One Team
            builder.HasOne(t => t.Team)
                   .WithMany(team => team.Tasks)
                   .HasForeignKey(t => t.TeamId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
