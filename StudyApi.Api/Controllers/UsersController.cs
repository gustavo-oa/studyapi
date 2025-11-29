using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Application.Users.Commands;
using StudyApi.Application.Users.Models;
using StudyApi.Application.Users.Queries;

namespace StudyApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken ct)
        {
            var created = await _mediator.Send(new CreateUserCommand(request.Name, request.Email, request.Password), ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id), ct);
            return user is null ? NotFound() : Ok(user);
        }

            [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name, CancellationToken ct)
        {
            var user = await _mediator.Send(new GetUserByNameQuery(name), ct);
            return user is null ? NotFound() : Ok(user);
        }

        // -----------------------------

        // -----------------------------

        public record UpdateUserRequest(string Name,string Email, bool IsEnabled);

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request, CancellationToken ct)
        {
            var updated = await _mediator.Send(new UpdateUserCommand(id, request.Name, request.Email, request.IsEnabled), ct);
            return updated is null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteUserCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
