namespace UserService.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public int OrganizationId { get; set; }
    }
}