using MediatR;
using Shared.Contracts.UserContracts;

namespace UserService.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public string Email { get; set; } = null!;
    }
}