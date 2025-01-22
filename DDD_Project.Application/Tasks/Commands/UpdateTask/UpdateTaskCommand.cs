using System;
using MediatR;
using DDD_Project.Application.Common.Models;
using DDD_Project.Domain.Enums;

namespace DDD_Project.Application.Tasks.Commands.UpdateTask
{
    public record UpdateTaskCommand : IRequest<Result<Unit>>
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public TaskPriority Priority { get; init; }
        public DDD_Project.Domain.Enums.TaskStatus Status { get; init; }
        public DateTime DueDate { get; init; }
        public Guid AssignedToId { get; init; }
    }
} 