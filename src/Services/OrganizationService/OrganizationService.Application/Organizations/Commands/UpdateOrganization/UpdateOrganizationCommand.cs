using MediatR;

namespace OrganizationService.Application.Organizations.Commands.UpdateOrganization
{
    public class UpdateOrganizationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}