using MediatR;

namespace DocumentService.Application.Documents.Commands.DeleteDocument
{
    public class DeleteDocumentCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        /// <summary>
        /// This will be removed when authentication is added where we will be able to pull the user information from authorization context
        /// </summary>
        public int CurrentUserId { get; set; }
    }
}