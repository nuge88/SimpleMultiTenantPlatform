using AutoMapper;
using DocumentService.Application.Common.Interfaces;
using DocumentService.Domain.Entities;
using DocumentService.Domain.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace DocumentService.Application.Documents.Commands.CreateOrganizationDocument
{
    public class CreateOrganizationDocumentCommandHandler(IDocumentRepository documentRepository,
        IUserServiceClient userServiceClient, IAuditRepository auditRepository,
        IMapper mapper)
        : IRequestHandler<CreateOrganizationDocumentCommand, int>
    {
        public async Task<int> Handle(CreateOrganizationDocumentCommand request, CancellationToken cancellationToken)
        {
            var user = await userServiceClient.GetUser(request.CurrentUserId);
            if (user == null)
            {
                throw new NotFoundException("User", request.CurrentUserId);
            }

            // The user can only add organization documents if they are assigned to one.
            if (user.OrganizationId <= 0)
            {
                throw new UserNotPartOfOrganizationException(user.Id);
            }

            var document = mapper.Map<Document>(request);
            document.OrganizationId = user.OrganizationId;

            await documentRepository.AddAsync(document);

            // Add the audit entry
            var audit = new Audit
            {
                EntityId = document.Id,
                EntityType = "Document",
                UserId = user.Id,
                Action = "Create",
                Details = $"Document '{document.Name}' created by user {request.CurrentUserId}",
                Timestamp = DateTime.UtcNow
            };

            await auditRepository.AddAuditAsync(audit);

            return document.Id;
        }
    }
}