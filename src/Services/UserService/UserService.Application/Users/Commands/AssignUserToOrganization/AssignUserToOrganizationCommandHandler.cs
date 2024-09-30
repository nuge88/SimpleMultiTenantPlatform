using MediatR;
using Shared.Exceptions;
using UserService.Domain.Interfaces;
using UserService.Application.Common.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Users.Commands.AssignUserToOrganization
{
    public class AssignUserToOrganizationCommandHandler(
        IUserRepository userRepository,
        IOrganizationServiceClient organizationServiceClient)
        : IRequestHandler<AssignUserToOrganizationCommand, Unit>
    {

        public async Task<Unit> Handle(AssignUserToOrganizationCommand request, CancellationToken cancellationToken)
        {
            // Validate User
            var user = await userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            // Validate Organization
            var organizationExists = await organizationServiceClient.OrganizationExistsAsync(request.OrganizationId);
            if (!organizationExists)
            {
                throw new NotFoundException("Organization", request.OrganizationId);
            }

            // Assign Organization to User
            user.OrganizationId = request.OrganizationId;

            await userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}