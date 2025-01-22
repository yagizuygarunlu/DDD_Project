using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using DDD_Project.Application.Common.Interfaces;
using DDD_Project.Application.Common.Models;
using DDD_Project.Application.Tasks.Commands.UpdateTask;
using DDD_Project.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DDD_Project.Tests.Application.Tasks.Commands.UpdateTask
{
    [TestClass]
    public class UpdateTaskCommandHandlerTests
    {
        private readonly IApplicationDbContext _context;
        private readonly UpdateTaskCommandHandler _handler;

        public UpdateTaskCommandHandlerTests()
        {
            _context = Substitute.For<IApplicationDbContext>();
            _handler = new UpdateTaskCommandHandler(_context);
        }

        [TestMethod]
        public async Task Handle_WhenTaskExists_ShouldUpdateTaskAndReturnSuccess()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var existingTask = new Domain.Aggregates.TaskAggregate.Task(
                "Old Title",
                "Old Description",
                TaskPriority.Low,
                DateTime.UtcNow,
                Guid.NewGuid());

            var command = new UpdateTaskCommand
            {
                Id = taskId,
                Title = "Updated Title",
                Description = "Updated Description",
                Priority = TaskPriority.High,
                Status = Domain.Enums.TaskStatus.InProgress,
                DueDate = DateTime.UtcNow.AddDays(1),
                AssignedToId = Guid.NewGuid()
            };

            var dbSetMock = Substitute.For<DbSet<Domain.Aggregates.TaskAggregate.Task>>();
            dbSetMock.FirstOrDefaultAsync(Arg.Any<System.Linq.Expressions.Expression<Func<Domain.Aggregates.TaskAggregate.Task, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(existingTask);

            _context.Tasks.Returns(dbSetMock);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBeTrue();
            await _context.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
            
            existingTask.Title.ShouldBe(command.Title);
            existingTask.Description.ShouldBe(command.Description);
            existingTask.Priority.ShouldBe(command.Priority);
            existingTask.Status.ShouldBe(command.Status);
            existingTask.DueDate.ShouldBe(command.DueDate);
            existingTask.AssignedToId.ShouldBe(command.AssignedToId);
        }

        [TestMethod]
        public async Task Handle_WhenTaskDoesNotExist_ShouldReturnFailure()
        {
            // Arrange
            var command = new UpdateTaskCommand { Id = Guid.NewGuid() };
            var dbSetMock = Substitute.For<DbSet<Domain.Aggregates.TaskAggregate.Task>>>();
            dbSetMock.FirstOrDefaultAsync(Arg.Any<System.Linq.Expressions.Expression<Func<Domain.Aggregates.TaskAggregate.Task, bool>>>(), Arg.Any<CancellationToken>())
                .Returns((Domain.Aggregates.TaskAggregate.Task)null);

            _context.Tasks.Returns(dbSetMock);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBeFalse();
            result.Errors.ShouldContain("Task not found");
            await _context.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
} 