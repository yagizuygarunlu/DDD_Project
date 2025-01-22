using System.Collections.Generic;
using MediatR;
using DDD_Project.Application.Common.Models;
using DDD_Project.Application.Tasks.Queries.Common;

namespace DDD_Project.Application.Tasks.Queries.GetAllTasks
{
    public record GetAllTasksQuery : IRequest<Result<IEnumerable<TaskDto>>>;
} 