using MediatR;
using OrganizationService.Domain.Entities;
using OrganizationService.Domain.Interfaces;
using Shared.Exceptions;

namespace OrganizationService.Application.Organizations.Commands.UpdateOrganization
{
    public class UpdateOrganizationCommandHandler(IOrganizationRepository organizationRepository)
        : IRequestHandler<UpdateOrganizationCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organization = await organizationRepository.GetByIdAsync(request.Id);

            if (organization == null)
            {
                throw new NotFoundException(nameof(Organization), request.Id);
            }

            // Update properties
            organization.Name = request.Name;

            await organizationRepository.UpdateAsync(organization);
            return Unit.Value;
        }
    }
}