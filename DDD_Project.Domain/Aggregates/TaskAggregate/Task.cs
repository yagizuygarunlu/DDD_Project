using System;
using System.Collections.Generic;
using DDD_Project.Domain.Common;
using DDD_Project.Domain.Enums;
using DDD_Project.Domain.Events;

namespace DDD_Project.Domain.Aggregates.TaskAggregate
{
    public class Task : Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskPriority Priority { get; private set; }
        public DDD_Project.Domain.Enums.TaskStatus Status { get; private set; }
        public DateTime DueDate { get; private set; }
        public Guid AssignedToId { get; private set; }
        public List<TaskComment> Comments { get; private set; }
        public List<TaskLabel> Labels { get; private set; }
        private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected Task() { }

        public Task(string title, string description, TaskPriority priority, DateTime dueDate, Guid assignedToId)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description;
            Priority = priority;
            Status = DDD_Project.Domain.Enums.TaskStatus.ToDo;
            DueDate = dueDate;
            AssignedToId = assignedToId;
            Comments = new List<TaskComment>();
            Labels = new List<TaskLabel>();
        }

        public void Update(string title, string description, TaskPriority priority, 
            DDD_Project.Domain.Enums.TaskStatus status, DateTime dueDate, Guid assignedToId)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description;
            Priority = priority;
            
            if (Status != status)
            {
                var oldStatus = Status;
                Status = status;
                _domainEvents.Add(new TaskStatusChangedEvent(Id, oldStatus, status));
            }
            
            DueDate = dueDate;
            AssignedToId = assignedToId;
        }

        public void UpdateStatus(DDD_Project.Domain.Enums.TaskStatus newStatus)
        {
            var oldStatus = Status;
            Status = newStatus;
            _domainEvents.Add(new TaskStatusChangedEvent(Id, oldStatus, newStatus));
        }

        public void UpdatePriority(TaskPriority newPriority)
        {
            Priority = newPriority;
        }

        public void Reassign(Guid newAssigneeId)
        {
            AssignedToId = newAssigneeId;
        }

        public void AddComment(string content, Guid authorId)
        {
            Comments.Add(new TaskComment(content, authorId));
        }

        public void AddLabel(string name, string color)
        {
            Labels.Add(new TaskLabel(name, color));
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
} 