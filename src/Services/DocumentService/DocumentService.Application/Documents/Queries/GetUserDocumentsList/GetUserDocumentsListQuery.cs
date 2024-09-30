using DocumentService.Application.Documents.Models;
using MediatR;

namespace DocumentService.Application.Documents.Queries.GetUserDocumentsList
{
    public class GetUserDocumentsListQuery : IRequest<List<DocumentDto>>
    {
        /// <summary>
        /// This property will be removed once authentication is implemented, 
        /// as user information will then be retrieved from the authorization context.
        /// </summary>
        public int CurrentUserId { get; set; }
    }
}
