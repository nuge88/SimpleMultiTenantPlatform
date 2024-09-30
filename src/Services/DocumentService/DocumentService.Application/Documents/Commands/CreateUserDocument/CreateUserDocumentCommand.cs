using MediatR;

namespace DocumentService.Application.Documents.Commands.CreateUserDocument
{
    public class CreateUserDocumentCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public string StoragePath { get; set; } = null!;

        /// <summary>
        /// This will be removed when authentication is added where we will be able to pull the user information from authorization context
        /// </summary>
        public int CurrentUserId { get; set; }
    }
}