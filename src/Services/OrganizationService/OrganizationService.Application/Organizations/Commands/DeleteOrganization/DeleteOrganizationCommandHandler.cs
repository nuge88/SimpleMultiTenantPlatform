using MediatR;
using OrganizationService.Domain.Entities;
using OrganizationService.Domain.Interfaces;
using Shared.Exceptions;

namespace OrganizationService.Application.Organizations.Commands.DeleteOrganization
{
    public class DeleteOrganizationCommandHandler(IOrganizationRepository organizationRepository)
        : IRequestHandler<DeleteOrganizationCommand>
    {
        public async Task Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organization = await organizationRepository.GetByIdAsync(request.Id);

            if (organization == null)
            {
                throw new NotFoundException(nameof(Organization), request.Id);
            }

            await organizationRepository.DeleteAsync(organization);
        }
    }
}