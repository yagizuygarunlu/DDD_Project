using System;

namespace DDD_Project.Domain.Common
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
} 