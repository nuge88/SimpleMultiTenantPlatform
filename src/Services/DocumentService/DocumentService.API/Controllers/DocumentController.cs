using DocumentService.Application.Documents.Commands.CreateUserDocument;
using DocumentService.Application.Documents.Commands.CreateOrganizationDocument;
using DocumentService.Application.Documents.Commands.UpdateDocument;
using DocumentService.Application.Documents.Commands.DeleteDocument;
using DocumentService.Application.Documents.Queries.GetDocumentById;
using DocumentService.Application.Documents.Queries.GetOrganizationDocumentsList;
using DocumentService.Application.Documents.Queries.GetUserDocumentsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DocumentService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController(IMediator mediator) : ControllerBase
    {
        // GET: api/documents/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(int id, [FromQuery] int currentUserId)
        {
            var query = new GetDocumentByIdQuery { Id = id, CurrentUserId = currentUserId };
            var document = await mediator.Send(query);
            return Ok(document);
        }

        // GET: api/organization-documents
        [HttpGet("organization-documents")]
        public async Task<IActionResult> GetOrganizationDocumentsList([FromQuery] int currentUserId)
        {
            var query = new GetOrganizationDocumentsListQuery { CurrentUserId = currentUserId };
            var documents = await mediator.Send(query);
            return Ok(documents);
        }

        // GET: api/user-documents
        [HttpGet("user-documents")]
        public async Task<IActionResult> GetUserDocumentsList([FromQuery] int currentUserId)
        {
            var query = new GetUserDocumentsListQuery { CurrentUserId = currentUserId };
            var documents = await mediator.Send(query);
            return Ok(documents);
        }
        // POST: api/documents
        [HttpPost("organization-document")]
        public async Task<IActionResult> CreateOrganizationDocument([FromBody] CreateOrganizationDocumentCommand command)
        {
            var documentId = await mediator.Send(command);
            return CreatedAtAction(nameof(CreateOrganizationDocument), new { id = documentId }, null);
        }

        // POST: api/documents
        [HttpPost("user-document")]
        public async Task<IActionResult> CreateUserDocument([FromBody] CreateUserDocumentCommand command)
        {
            var documentId = await mediator.Send(command);
            return CreatedAtAction(nameof(CreateUserDocument), new { id = documentId }, null);
        }

        // PUT: api/documents/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] UpdateDocumentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Document ID mismatch");
            }

            await mediator.Send(command);
            return NoContent(); // 204 No Content
        }

        // DELETE: api/documents/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id, [FromQuery] int currentUserId)
        {
            var command = new DeleteDocumentCommand { Id = id, CurrentUserId = currentUserId };
            await mediator.Send(command);
            return NoContent();
        }

    }
}
