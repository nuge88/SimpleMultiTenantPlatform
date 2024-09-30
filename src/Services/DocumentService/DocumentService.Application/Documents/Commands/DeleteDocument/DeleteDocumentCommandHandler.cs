using DocumentService.Application.Common.Interfaces;
using MediatR;
using Shared.Exceptions;
using DocumentService.Domain.Interfaces;
using DocumentService.Domain.Entities;

namespace DocumentService.Application.Documents.Commands.DeleteDocument
{
    public class DeleteDocumentCommandHandler(IDocumentRepository documentRepository, IUserServiceClient userServiceClient, IAuditRepository auditRepository) : IRequestHandler<DeleteDocumentCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var user = await userServiceClient.GetUser(request.CurrentUserId);
            if (user == null)
            {
                throw new NotFoundException("User", request.CurrentUserId);
            }

            var document = await documentRepository.GetByIdAsync(request.Id, user.Id, user.OrganizationId);
            if (document == null)
            {
                throw new NotFoundException(nameof(document), request.Id);
            }

            // Authorization check: make sure the user is allowed to update the document
            if (document.UserId != request.CurrentUserId && document.OrganizationId != user.OrganizationId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this document.");
            }

            await documentRepository.DeleteAsync(document);

            // Add the audit entry
            var audit = new Audit
            {
                EntityId = document.Id,
                EntityType = "Document",
                UserId = user.Id,
                Action = "Delete",
                Details = $"Document '{document.Name}' deleted by user {request.CurrentUserId}",
                Timestamp = DateTime.UtcNow
            };

            await auditRepository.AddAuditAsync(audit);

            return Unit.Value;
        }
    }
}