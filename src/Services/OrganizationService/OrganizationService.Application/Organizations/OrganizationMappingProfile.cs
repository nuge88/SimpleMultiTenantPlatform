using AutoMapper;
using OrganizationService.Application.Organizations.Models;
using OrganizationService.Domain.Entities;

namespace OrganizationService.Application.Organizations
{
    public class OrganizationMappingProfile : Profile
    {
        public OrganizationMappingProfile()
        {
            CreateMap<Organization, OrganizationDto>();
        }
    }
}