using FluentValidation;

namespace DocumentService.Application.Documents.Commands.CreateUserDocument
{
    public class CreateUserDocumentCommandValidator : AbstractValidator<CreateUserDocumentCommand>
    {
        public CreateUserDocumentCommandValidator()
        {
        }
    }
}