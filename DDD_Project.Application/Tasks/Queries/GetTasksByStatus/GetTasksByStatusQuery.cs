using System.Collections.Generic;
using MediatR;
using DDD_Project.Application.Common.Models;
using DDD_Project.Application.Tasks.Queries.Common;
using DDD_Project.Domain.Enums;

namespace DDD_Project.Application.Tasks.Queries.GetTasksByStatus
{
    public record GetTasksByStatusQuery(DDD_Project.Domain.Enums.TaskStatus Status) : IRequest<Result<IEnumerable<TaskDto>>>;
} 