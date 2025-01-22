using System;
using MediatR;
using DDD_Project.Application.Common.Models;
using DDD_Project.Application.Tasks.Queries.Common;

namespace DDD_Project.Application.Tasks.Queries.GetTaskById
{
    public record GetTaskByIdQuery(Guid Id) : IRequest<Result<TaskDto>>;
} 