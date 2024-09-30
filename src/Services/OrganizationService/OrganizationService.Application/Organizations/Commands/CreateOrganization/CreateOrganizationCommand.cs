using MediatR;

namespace OrganizationService.Application.Organizations.Commands.CreateOrganization
{
    public class CreateOrganizationCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}