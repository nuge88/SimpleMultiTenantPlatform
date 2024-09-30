using MediatR;
using OrganizationService.Application.Organizations.Models;

namespace OrganizationService.Application.Organizations.Queries.GetOrganizationsList
{
    public class GetOrganizationsListQuery : IRequest<List<OrganizationDto>>
    {
    }
}