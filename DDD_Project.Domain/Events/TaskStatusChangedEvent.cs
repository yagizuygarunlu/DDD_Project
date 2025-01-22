using System;
using DDD_Project.Domain.Common;
using DDD_Project.Domain.Enums;

namespace DDD_Project.Domain.Events
{
    public class TaskStatusChangedEvent : IDomainEvent
    {
        public Guid TaskId { get; }
        public DDD_Project.Domain.Enums.TaskStatus OldStatus { get; }
        public DDD_Project.Domain.Enums.TaskStatus NewStatus { get; }
        public DateTime OccurredOn { get; }

        public TaskStatusChangedEvent(Guid taskId, DDD_Project.Domain.Enums.TaskStatus oldStatus, DDD_Project.Domain.Enums.TaskStatus newStatus)
        {
            TaskId = taskId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            OccurredOn = DateTime.UtcNow;
        }
    }
} 