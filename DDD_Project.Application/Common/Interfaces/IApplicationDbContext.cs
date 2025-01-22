using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DDD_Project.Domain.Aggregates.TaskAggregate;

namespace DDD_Project.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Domain.Aggregates.TaskAggregate.Task> Tasks { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
} 