using MediatR;
using Shared.Contracts.UserContracts;

namespace UserService.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int Id { get; set; }
    }
}