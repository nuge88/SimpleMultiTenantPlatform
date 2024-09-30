using AutoMapper;
using DocumentService.Application.Common.Interfaces;
using MediatR;
using DocumentService.Application.Documents.Models;
using DocumentService.Domain.Interfaces;
using Shared.Exceptions;
using DocumentService.Domain.Entities;

namespace DocumentService.Application.Documents.Queries.GetDocumentById
{
    public class GetDocumentByIdQueryHandler(IDocumentRepository documentRepository, IUserServiceClient userServiceClient, IMapper mapper)
        : IRequestHandler<GetDocumentByIdQuery, DocumentDto>
    {
        public async Task<DocumentDto> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userServiceClient.GetUser(request.CurrentUserId);
            if (user == null)
            {
                throw new NotFoundException("User", request.CurrentUserId);
            }

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

            return mapper.Map<DocumentDto>(document);
        }
    }
}