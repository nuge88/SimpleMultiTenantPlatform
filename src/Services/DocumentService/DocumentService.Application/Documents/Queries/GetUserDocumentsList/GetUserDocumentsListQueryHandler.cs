using AutoMapper;
using DocumentService.Application.Common.Interfaces;
using DocumentService.Application.Documents.Models;
using DocumentService.Domain.Interfaces;
using MediatR;
using Shared.Exceptions;

namespace DocumentService.Application.Documents.Queries.GetUserDocumentsList
{
    public class GetDocumentsListQueryHandler(IDocumentRepository documentRepository, IUserServiceClient userServiceClient, IMapper mapper)
        : IRequestHandler<GetUserDocumentsListQuery, List<DocumentDto>>
    {
        public async Task<List<DocumentDto>> Handle(GetUserDocumentsListQuery request, CancellationToken cancellationToken)
        {
            var user = await userServiceClient.GetUser(request.CurrentUserId);
            if (user == null)
            {
                throw new NotFoundException("User", request.CurrentUserId);
            }

            // Fetch documents owned by the user or the organization
            var documents = await documentRepository.GetAllByUserAsync(user.Id);

            // Map documents to DocumentDto
            var documentDtos = mapper.Map<List<DocumentDto>>(documents);

            return documentDtos;
        }
    }
}