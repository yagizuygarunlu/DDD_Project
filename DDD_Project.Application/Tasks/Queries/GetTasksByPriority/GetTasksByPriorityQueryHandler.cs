using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DDD_Project.Application.Common.Interfaces;
using DDD_Project.Application.Common.Models;
using DDD_Project.Application.Tasks.Queries.Common;

namespace DDD_Project.Application.Tasks.Queries.GetTasksByPriority
{
    public class GetTasksByPriorityQueryHandler : IRequestHandler<GetTasksByPriorityQuery, Result<IEnumerable<TaskDto>>>
    {
        private readonly IApplicationDbContext _context;

        public GetTasksByPriorityQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<TaskDto>>> Handle(GetTasksByPriorityQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _context.Tasks
                .Include(t => t.Comments)
                .Include(t => t.Labels)
                .Where(t => t.Priority == request.Priority)
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Priority = t.Priority,
                    Status = t.Status,
                    DueDate = t.DueDate,
                    AssignedToId = t.AssignedToId,
                    Comments = t.Comments.Select(c => new TaskCommentDto
                    {
                        Content = c.Content,
                        AuthorId = c.AuthorId,
                        CreatedAt = c.CreatedAt
                    }).ToList(),
                    Labels = t.Labels.Select(l => new TaskLabelDto
                    {
                        Name = l.Name,
                        Color = l.Color
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<TaskDto>>.Success(tasks);
        }
    }
} 