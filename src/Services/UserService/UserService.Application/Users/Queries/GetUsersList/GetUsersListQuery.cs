using MediatR;
using Shared.Contracts.UserContracts;

namespace UserService.Application.Users.Queries.GetUsersList
{
    public class GetUsersListQuery : IRequest<List<UserDto>>
    {
    }
}