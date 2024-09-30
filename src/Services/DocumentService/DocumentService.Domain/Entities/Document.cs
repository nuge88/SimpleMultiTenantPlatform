namespace DocumentService.Domain.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Size { get; set; }
        public string StoragePath { get; set; } = null!;
        public int? UserId { get; set; }
        public int? OrganizationId { get; set; }
    }
}