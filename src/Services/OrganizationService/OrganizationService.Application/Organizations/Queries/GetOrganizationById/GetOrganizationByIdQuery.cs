using MediatR;
using OrganizationService.Application.Organizations.Models;

namespace OrganizationService.Application.Organizations.Queries.GetOrganizationById
{
    public class GetOrganizationByIdQuery : IRequest<OrganizationDto>
    {
        public int Id { get; set; }
    }
}