using MediatR;
using OrganizationService.Domain.Entities;
using OrganizationService.Domain.Interfaces;

namespace OrganizationService.Application.Organizations.Commands.CreateOrganization
{
    public class CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository)
        : IRequestHandler<CreateOrganizationCommand, int>
    {
        public async Task<int> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organization = new Organization
            {
                Name = request.Name
            };

            await organizationRepository.AddAsync(organization);

            return organization.Id;
        }
    }
}