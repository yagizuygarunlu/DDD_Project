using System;
using System.Collections.Generic;
using DDD_Project.Domain.Common;

namespace DDD_Project.Domain.Aggregates.TaskAggregate
{
    public class TaskComment : ValueObject
    {
        public string Content { get; private set; }
        public Guid AuthorId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected TaskComment() { }

        public TaskComment(string content, Guid authorId)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
            AuthorId = authorId;
            CreatedAt = DateTime.UtcNow;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Content;
            yield return AuthorId;
            yield return CreatedAt;
        }
    }
} 