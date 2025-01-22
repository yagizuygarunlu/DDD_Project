using System.Collections.Generic;
using MediatR;
using DDD_Project.Application.Common.Models;
using DDD_Project.Application.Tasks.Queries.Common;
using DDD_Project.Domain.Enums;

namespace DDD_Project.Application.Tasks.Queries.GetTasksByPriority
{
    public record GetTasksByPriorityQuery(TaskPriority Priority) : IRequest<Result<IEnumerable<TaskDto>>>;
} 