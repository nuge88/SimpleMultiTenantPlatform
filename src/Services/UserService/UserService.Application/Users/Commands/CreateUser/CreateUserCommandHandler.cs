using MediatR;
using Shared.Contracts.UserContracts;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using AutoMapper;

namespace UserService.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<CreateUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Create a new user entity
            var user = mapper.Map<User>(request);

            // Add the user to the repository
            await userRepository.AddAsync(user);
            return mapper.Map<UserDto>(user);
        }
    }
}