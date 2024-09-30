using AutoMapper;
using MediatR;
using OrganizationService.Application.Organizations.Models;
using OrganizationService.Domain.Interfaces;
using Shared.Exceptions;

namespace OrganizationService.Application.Organizations.Queries.GetOrganizationById
{
    public class GetOrganizationByIdQueryHandler(IOrganizationRepository organizationRepository, IMapper mapper)
        : IRequestHandler<GetOrganizationByIdQuery, OrganizationDto>
    {
        public async Task<OrganizationDto> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
        {
            var organization = await organizationRepository.GetByIdAsync(request.Id);

            if (organization == null)
            {
                throw new NotFoundException(nameof(organization), request.Id);
            }

            return mapper.Map<OrganizationDto>(organization);
        }
    }
}