using FluentValidation;

namespace OrganizationService.Application.Organizations.Commands.UpdateOrganization
{
    public class UpdateOrganizationCommandValidator : AbstractValidator<UpdateOrganizationCommand>
    {
        public UpdateOrganizationCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Organization ID must be greater than zero.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Organization name is required.")
                .MaximumLength(100).WithMessage("Organization name must not exceed 100 characters.");
        }
    }
}