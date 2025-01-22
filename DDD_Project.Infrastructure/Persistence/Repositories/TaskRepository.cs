using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DDD_Project.Domain.Aggregates.TaskAggregate;
using DDD_Project.Domain.Enums;

namespace DDD_Project.Infrastructure.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Aggregates.TaskAggregate.Task> GetByIdAsync(Guid id)
        {
            return await _context.Tasks
                .Include(t => t.Comments)
                .Include(t => t.Labels)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Domain.Aggregates.TaskAggregate.Task>> GetAllAsync()
        {
            return await _context.Tasks
                .Include(t => t.Comments)
                .Include(t => t.Labels)
                .ToListAsync();
        }

        public async Task<IEnumerable<Domain.Aggregates.TaskAggregate.Task>> GetByAssigneeIdAsync(Guid assigneeId)
        {
            return await _context.Tasks
                .Include(t => t.Comments)
                .Include(t => t.Labels)
                .Where(t => t.AssignedToId == assigneeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Domain.Aggregates.TaskAggregate.Task>> GetByStatusAsync(DDD_Project.Domain.Enums.TaskStatus status)
        {
            return await _context.Tasks
                .Include(t => t.Comments)
                .Include(t => t.Labels)
                .Where(t => t.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Domain.Aggregates.TaskAggregate.Task>> GetByPriorityAsync(TaskPriority priority)
        {
            return await _context.Tasks
                .Include(t => t.Comments)
                .Include(t => t.Labels)
                .Where(t => t.Priority == priority)
                .ToListAsync();
        }

        public async Task<IEnumerable<Domain.Aggregates.TaskAggregate.Task>> GetOverdueTasks()
        {
            return await _context.Tasks
                .Include(t => t.Comments)
                .Include(t => t.Labels)
                .Where(t => t.DueDate < DateTime.UtcNow && t.Status != DDD_Project.Domain.Enums.TaskStatus.Done)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task AddAsync(Domain.Aggregates.TaskAggregate.Task entity)
        {
            await _context.Tasks.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Domain.Aggregates.TaskAggregate.Task entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(Domain.Aggregates.TaskAggregate.Task entity)
        {
            _context.Tasks.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
} 