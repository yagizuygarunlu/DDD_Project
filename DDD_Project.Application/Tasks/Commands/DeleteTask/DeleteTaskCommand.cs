using System;
using MediatR;
using DDD_Project.Application.Common.Models;

namespace DDD_Project.Application.Tasks.Commands.DeleteTask
{
    public record DeleteTaskCommand(Guid Id) : IRequest<Result<Unit>>;
} 