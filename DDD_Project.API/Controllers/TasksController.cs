using Microsoft.AspNetCore.Mvc;
using MediatR;
using DDD_Project.Application.Tasks.Commands.CreateTask;
using DDD_Project.Application.Tasks.Commands.UpdateTask;
using DDD_Project.Application.Tasks.Commands.DeleteTask;
using DDD_Project.Application.Tasks.Queries.GetTaskById;
using DDD_Project.Application.Tasks.Queries.GetAllTasks;
using DDD_Project.Application.Tasks.Queries.GetTasksByStatus;
using DDD_Project.Application.Tasks.Queries.GetTasksByPriority;
using DDD_Project.Application.Tasks.Queries.GetOverdueTasks;
using DDD_Project.Application.Tasks.Queries.Common;
using DDD_Project.Domain.Enums;

namespace DDD_Project.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetTaskByIdQuery(id));

            if (!result.Succeeded)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateTaskCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result.Data);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                return NotFound(result.Errors);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteTaskCommand(id));

            if (!result.Succeeded)
            {
                return NotFound(result.Errors);
            }

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllTasksQuery());
            return Ok(result.Data);
        }

        [HttpGet("by-status/{status}")]
        [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByStatus(DDD_Project.Domain.Enums.TaskStatus status)
        {
            var result = await _mediator.Send(new GetTasksByStatusQuery(status));
            return Ok(result.Data);
        }

        [HttpGet("by-priority/{priority}")]
        [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByPriority(TaskPriority priority)
        {
            var result = await _mediator.Send(new GetTasksByPriorityQuery(priority));
            return Ok(result.Data);
        }

        [HttpGet("overdue")]
        [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOverdueTasks()
        {
            var result = await _mediator.Send(new GetOverdueTasksQuery());
            return Ok(result.Data);
        }
    }
} 