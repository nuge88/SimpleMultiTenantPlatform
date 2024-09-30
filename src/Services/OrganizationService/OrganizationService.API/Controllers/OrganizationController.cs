using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Application.Organizations.Commands.CreateOrganization;
using OrganizationService.Application.Organizations.Commands.UpdateOrganization;
using OrganizationService.Application.Organizations.Commands.DeleteOrganization;
using OrganizationService.Application.Organizations.Queries.GetOrganizationById;
using OrganizationService.Application.Organizations.Queries.GetOrganizationsList;

namespace OrganizationService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizationById(int id)
        {
            var query = new GetOrganizationByIdQuery { Id = id };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrganizationsList()
        {
            var query = new GetOrganizationsListQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationCommand command)
        {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetOrganizationById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganization(int id, [FromBody] UpdateOrganizationCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID in URL does not match ID in request body.");
            }

            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            var command = new DeleteOrganizationCommand { Id = id };
            await mediator.Send(command);
            return NoContent();
        }
    }
}
