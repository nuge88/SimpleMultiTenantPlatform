using AutoMapper;
using DocumentService.Application.Documents.Commands.CreateOrganizationDocument;
using DocumentService.Application.Documents.Commands.CreateUserDocument;
using DocumentService.Application.Documents.Commands.UpdateDocument;
using DocumentService.Application.Documents.Models;
using DocumentService.Domain.Entities;

namespace DocumentService.Application.Documents
{
    public class OrganizationMappingProfile : Profile
    {
        public OrganizationMappingProfile()
        {
            CreateMap<CreateOrganizationDocumentCommand, Document>();
            CreateMap<CreateUserDocumentCommand, Document>();
            CreateMap<UpdateDocumentCommand, Document>();
            CreateMap<Document, DocumentDto>();
        }
    }
}