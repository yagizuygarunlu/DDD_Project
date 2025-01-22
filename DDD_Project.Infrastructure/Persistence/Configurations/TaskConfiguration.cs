using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDD_Project.Domain.Aggregates.TaskAggregate;

namespace DDD_Project.Infrastructure.Persistence.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Domain.Aggregates.TaskAggregate.Task>
    {
        public void Configure(EntityTypeBuilder<Domain.Aggregates.TaskAggregate.Task> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(2000);

            builder.Property(t => t.Status)
                .IsRequired();

            builder.Property(t => t.Priority)
                .IsRequired();

            builder.Property(t => t.DueDate)
                .IsRequired();

            builder.Property(t => t.AssignedToId)
                .IsRequired();

            // Configure owned entities (Value Objects)
            builder.OwnsMany(t => t.Comments, cb =>
            {
                cb.WithOwner().HasForeignKey("TaskId");
                cb.Property<int>("Id").ValueGeneratedOnAdd();
                cb.HasKey("Id");
                
                cb.Property(c => c.Content)
                    .HasMaxLength(1000)
                    .IsRequired();
                
                cb.Property(c => c.AuthorId)
                    .IsRequired();
                
                cb.Property(c => c.CreatedAt)
                    .IsRequired();
            });

            builder.OwnsMany(t => t.Labels, lb =>
            {
                lb.WithOwner().HasForeignKey("TaskId");
                lb.Property<int>("Id").ValueGeneratedOnAdd();
                lb.HasKey("Id");
                
                lb.Property(l => l.Name)
                    .HasMaxLength(50)
                    .IsRequired();
                
                lb.Property(l => l.Color)
                    .HasMaxLength(7)
                    .IsRequired();
            });
        }
    }
} 