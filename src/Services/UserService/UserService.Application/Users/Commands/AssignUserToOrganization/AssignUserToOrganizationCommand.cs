using MediatR;

namespace UserService.Application.Users.Commands.AssignUserToOrganization
{
    public class AssignUserToOrganizationCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
    }
}