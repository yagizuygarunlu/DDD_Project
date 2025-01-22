using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDD_Project.Domain.Common;
using DDD_Project.Domain.Enums;

namespace DDD_Project.Domain.Aggregates.TaskAggregate
{
    public interface ITaskRepository : IRepository<Task>
    {
        Task<IEnumerable<Task>> GetByAssigneeIdAsync(Guid assigneeId);
        Task<IEnumerable<Task>> GetByStatusAsync(DDD_Project.Domain.Enums.TaskStatus status);
        Task<IEnumerable<Task>> GetByPriorityAsync(TaskPriority priority);
        Task<IEnumerable<Task>> GetOverdueTasks();
    }
} 