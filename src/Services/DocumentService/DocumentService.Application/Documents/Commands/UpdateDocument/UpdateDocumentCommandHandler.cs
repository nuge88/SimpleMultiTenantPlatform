using DocumentService.Application.Common.Interfaces;
using DocumentService.Domain.Entities;
using DocumentService.Domain.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace DocumentService.Application.Documents.Commands.UpdateDocument
{
    public class UpdateDocumentCommandHandler(IDocumentRepository documentRepository, IUserServiceClient userServiceClient, IAuditRepository auditRepository)
        : IRequestHandler<UpdateDocumentCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var user = await userServiceClient.GetUser(request.CurrentUserId);
            if (user == null)
            {
                throw new NotFoundException("User", request.CurrentUserId);
            }

            // Fetch the document from the repository
            var document = await documentRepository.GetByIdAsync(request.Id, user.Id, user.OrganizationId);

            if (document == null)
            {
                throw new NotFoundException(nameof(Document), request.Id);
            }

            // Authorization check: make sure the user is allowed to update the document
            if (document.UserId != request.CurrentUserId && document.OrganizationId != user.OrganizationId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this document.");
            }

            // Update document properties
            document.Name = request.Name;
            document.Size = request.Size;
            document.StoragePath = request.StoragePath;

            // Save changes
            await documentRepository.UpdateAsync(document);

            // Add the audit entry
            var audit = new Audit
            {
                EntityId = document.Id,
                EntityType = "Document",
                UserId = user.Id,
                Action = "Update",
                Details = $"Document '{document.Name}' updated by user {request.CurrentUserId}",
                Timestamp = DateTime.UtcNow
            };

            await auditRepository.AddAuditAsync(audit);

            return Unit.Value;
        }
    }
}