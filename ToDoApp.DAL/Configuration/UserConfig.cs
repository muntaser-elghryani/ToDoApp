using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Phone)
                   .IsRequired()
                   .HasMaxLength(20);
            builder.HasIndex(u => u.Phone)
                .IsUnique();

            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(u => u.TeamId)
                .IsRequired(false);

            builder.Property(u => u.Status)
                    .HasColumnType("varchar(50)")
                   .IsRequired();

            builder.Property(u => u.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");

            // Relationships

            builder.HasOne(r => r.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(u => u.Team)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.TeamId)
                .OnDelete(DeleteBehavior.SetNull);




        }
    }
}
