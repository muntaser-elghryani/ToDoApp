using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.DAL.Enums;

namespace ToDoApp.DAL.Entities
{
    public class TaskItem
    {
        // Primary Key
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public enTaskStatus Status { get; set; }

        // FK: المستخدم المكلف بالمهمة
        public int AssignedToId { get; set; }
        public User AssignedTo { get; set; } = null!;

        // FK: الفريق
        public int TeamId { get; set; }
        public Team Team { get; set; } = null!; // Navigation property

        // FK: المدير الذي أنشأ المهمة
        public int CreatedById { get; set; }

        // تاريخ الاستحقاق
        public DateTime DueDate { get; set; }

        // تاريخ الإنشاء
        public DateTime CreatedAt { get; set; }

        // تاريخ التحديث
        public DateTime UpdatedAt { get; set; }
    }
}
