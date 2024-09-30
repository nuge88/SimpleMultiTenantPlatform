namespace Shared.Contracts.UserContracts
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public int OrganizationId { get; set; }
    }
}