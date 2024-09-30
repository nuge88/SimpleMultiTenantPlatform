using AutoMapper;
using MediatR;
using OrganizationService.Application.Organizations.Models;
using OrganizationService.Domain.Interfaces;

namespace OrganizationService.Application.Organizations.Queries.GetOrganizationsList
{
    public class GetOrganizationsListQueryHandler(IOrganizationRepository organizationRepository, IMapper mapper)
        : IRequestHandler<GetOrganizationsListQuery, List<OrganizationDto>>
    {
        public async Task<List<OrganizationDto>> Handle(GetOrganizationsListQuery request, CancellationToken cancellationToken)
        {
            var organizations = await organizationRepository.GetAllAsync();
            return mapper.Map<List<OrganizationDto>>(organizations);
        }
    }
}