using MediatR;
using DocumentService.Application.Documents.Models;

namespace DocumentService.Application.Documents.Queries.GetDocumentById
{
    public class GetDocumentByIdQuery : IRequest<DocumentDto>
    {
        /// <summary>
        /// This property will be removed once authentication is implemented, 
        /// as user information will then be retrieved from the authorization context.
        /// </summary>
        public int CurrentUserId { get; set; }
        public int Id { get; set; }
    }
}