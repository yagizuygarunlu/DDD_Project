using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using DDD_Project.Application.Common.Interfaces;
using DDD_Project.Application.Common.Models;
using DDD_Project.Application.Tasks.Queries.Common;

namespace DDD_Project.Application.Tasks.Queries.GetTaskById
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, Result<TaskDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetTaskByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<TaskDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks
                .Include(t => t.Comments)
                .Include(t => t.Labels)
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (task == null)
            {
                return Result<TaskDto>.Failure(new[] { "Task not found" });
            }

            var taskDto = new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                Status = task.Status,
                DueDate = task.DueDate,
                AssignedToId = task.AssignedToId,
                Comments = task.Comments.Select(c => new TaskCommentDto
                {
                    Content = c.Content,
                    AuthorId = c.AuthorId,
                    CreatedAt = c.CreatedAt
                }).ToList(),
                Labels = task.Labels.Select(l => new TaskLabelDto
                {
                    Name = l.Name,
                    Color = l.Color
                }).ToList()
            };

            return Result<TaskDto>.Success(taskDto);
        }
    }
} 