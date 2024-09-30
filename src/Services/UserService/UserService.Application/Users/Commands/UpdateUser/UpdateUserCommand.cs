using MediatR;

namespace UserService.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
    }
}