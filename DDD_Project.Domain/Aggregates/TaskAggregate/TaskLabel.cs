using System;
using System.Collections.Generic;
using DDD_Project.Domain.Common;

namespace DDD_Project.Domain.Aggregates.TaskAggregate
{
    public class TaskLabel : ValueObject
    {
        public string Name { get; private set; }
        public string Color { get; private set; }

        protected TaskLabel() { }

        public TaskLabel(string name, string color)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Color = color ?? throw new ArgumentNullException(nameof(color));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Color;
        }
    }
} 