using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using DDD_Project.Application.Common.Interfaces;
using DDD_Project.Application.Common.Models;
using DDD_Project.Application.Tasks.Commands.DeleteTask;
using DDD_Project.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DDD_Project.Tests.Application.Tasks.Commands.DeleteTask
{
    [TestClass]
    public class DeleteTaskCommandHandlerTests
    {
        private readonly IApplicationDbContext _context;
        private readonly DeleteTaskCommandHandler _handler;

        public DeleteTaskCommandHandlerTests()
        {
            _context = Substitute.For<IApplicationDbContext>();
            _handler = new DeleteTaskCommandHandler(_context);
        }

        [TestMethod]
        public async Task Handle_WhenTaskExists_ShouldDeleteTaskAndReturnSuccess()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var existingTask = new Domain.Aggregates.TaskAggregate.Task(
                "Test Title",
                "Test Description",
                TaskPriority.Medium,
                DateTime.UtcNow,
                Guid.NewGuid());

            var command = new DeleteTaskCommand(taskId);

            var dbSetMock = Substitute.For<DbSet<Domain.Aggregates.TaskAggregate.Task>>();
            dbSetMock.FirstOrDefaultAsync(Arg.Any<System.Linq.Expressions.Expression<Func<Domain.Aggregates.TaskAggregate.Task, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(existingTask);

            _context.Tasks.Returns(dbSetMock);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBeTrue();
            _context.Tasks.Received(1).Remove(existingTask);
            await _context.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [TestMethod]
        public async Task Handle_WhenTaskDoesNotExist_ShouldReturnFailure()
        {
            // Arrange
            var command = new DeleteTaskCommand(Guid.NewGuid());
            var dbSetMock = Substitute.For<DbSet<Domain.Aggregates.TaskAggregate.Task>>>();
            dbSetMock.FirstOrDefaultAsync(Arg.Any<System.Linq.Expressions.Expression<Func<Domain.Aggregates.TaskAggregate.Task, bool>>>(), Arg.Any<CancellationToken>())
                .Returns((Domain.Aggregates.TaskAggregate.Task)null);

            _context.Tasks.Returns(dbSetMock);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBeFalse();
            result.Errors.ShouldContain("Task not found");
            _context.Tasks.DidNotReceive().Remove(Arg.Any<Domain.Aggregates.TaskAggregate.Task>());
            await _context.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
} 