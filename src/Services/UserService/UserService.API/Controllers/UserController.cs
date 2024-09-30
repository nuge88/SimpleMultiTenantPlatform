using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Users.Commands.AssignUserToOrganization;
using UserService.Application.Users.Commands.CreateUser;
using UserService.Application.Users.Commands.DeleteUser;
using UserService.Application.Users.Commands.UpdateUser;
using UserService.Application.Users.Queries.GetUserById;
using UserService.Application.Users.Queries.GetUsersList;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetUsersList()
        {
            var query = new GetUsersListQuery();
            var users = await mediator.Send(query);
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var user = await mediator.Send(query);

            // Exception handling middleware will handle NotFoundException
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var user = await mediator.Send(command);

            // Return 201 Created with the location of the new resource
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID in URL does not match ID in request body.");
            }

            await mediator.Send(command);

            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var command = new DeleteUserCommand { Id = id };
            await mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{userId}/assign-organization")]
        public async Task<IActionResult> AssignUserToOrganization(int userId, [FromBody] AssignUserToOrganizationCommand command)
        {
            if (userId != command.UserId)
            {
                return BadRequest("User ID in URL does not match User ID in request body.");
            }

            await mediator.Send(command);
            return NoContent();
        }

    }
}
