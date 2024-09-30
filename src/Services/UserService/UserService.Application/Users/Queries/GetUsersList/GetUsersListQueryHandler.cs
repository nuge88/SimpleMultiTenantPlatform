using MediatR;
using Shared.Contracts.UserContracts;
using UserService.Domain.Interfaces;
using AutoMapper;

namespace UserService.Application.Users.Queries.GetUsersList
{
    public class GetUsersListQueryHandler(IUserRepository userRepository, IMapper mapper)
        : IRequestHandler<GetUsersListQuery, List<UserDto>>
    {
        public async Task<List<UserDto>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var users = await userRepository.GetAllAsync();
            return mapper.Map<List<UserDto>>(users);
        }
    }
}