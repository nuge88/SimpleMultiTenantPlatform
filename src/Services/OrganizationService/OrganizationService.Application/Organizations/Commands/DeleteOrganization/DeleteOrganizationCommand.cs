using MediatR;

namespace OrganizationService.Application.Organizations.Commands.DeleteOrganization
{
    public class DeleteOrganizationCommand : IRequest
    {
        public int Id { get; set; }
    }
}