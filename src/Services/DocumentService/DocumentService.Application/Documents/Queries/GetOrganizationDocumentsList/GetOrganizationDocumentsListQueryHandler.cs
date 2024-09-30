using AutoMapper;
using DocumentService.Application.Common.Interfaces;
using DocumentService.Application.Documents.Models;
using DocumentService.Domain.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace DocumentService.Application.Documents.Queries.GetOrganizationDocumentsList
{
    public class GetDocumentsListQueryHandler(IDocumentRepository documentRepository,IUserServiceClient userServiceClient, IMapper mapper)
        : IRequestHandler<GetOrganizationDocumentsListQuery, List<DocumentDto>>
    {
        public async Task<List<DocumentDto>> Handle(GetOrganizationDocumentsListQuery request, CancellationToken cancellationToken)
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

            // Fetch documents owned by the user or the organization
            var documents = await documentRepository.GetAllByOrganizationAsync(user.OrganizationId);

            // Map documents to DocumentDto
            var documentDtos = mapper.Map<List<DocumentDto>>(documents);

            return documentDtos;
        }
    }
}