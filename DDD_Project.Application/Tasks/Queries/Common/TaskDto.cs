using System;
using System.Collections.Generic;
using DDD_Project.Domain.Enums;

namespace DDD_Project.Application.Tasks.Queries.Common
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
        public DDD_Project.Domain.Enums.TaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public Guid AssignedToId { get; set; }
        public List<TaskCommentDto> Comments { get; set; } = new();
        public List<TaskLabelDto> Labels { get; set; } = new();
    }

    public class TaskCommentDto
    {
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class TaskLabelDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
} 