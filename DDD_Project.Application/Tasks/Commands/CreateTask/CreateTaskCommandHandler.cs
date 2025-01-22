using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using DDD_Project.Application.Common.Interfaces;
using DDD_Project.Application.Common.Models;

namespace DDD_Project.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result<Guid>>
    {
        private readonly IApplicationDbContext _context;

        public CreateTaskCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Aggregates.TaskAggregate.Task(
                request.Title,
                request.Description,
                request.Priority,
                request.DueDate,
                request.AssignedToId);

            _context.Tasks.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(entity.Id);
        }
    }
} 