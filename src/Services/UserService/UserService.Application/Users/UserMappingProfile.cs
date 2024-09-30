using AutoMapper;
using Shared.Contracts.UserContracts;
using UserService.Application.Users.Commands.CreateUser;
using UserService.Application.Users.Commands.UpdateUser;
using UserService.Domain.Entities;


namespace UserService.Application.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();
            CreateMap<User, UserDto>();
        }
    }
}