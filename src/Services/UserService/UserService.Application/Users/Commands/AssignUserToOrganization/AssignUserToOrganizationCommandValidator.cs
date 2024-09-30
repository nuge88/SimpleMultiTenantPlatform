using FluentValidation;

namespace UserService.Application.Users.Commands.AssignUserToOrganization
{
    public class AssignUserToOrganizationCommandValidator : AbstractValidator<AssignUserToOrganizationCommand>
    {
        public AssignUserToOrganizationCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("User ID must be greater than zero.");

            RuleFor(x => x.OrganizationId)
                .GreaterThan(0).WithMessage("Organization ID must be greater than zero.");

        }
    }
}