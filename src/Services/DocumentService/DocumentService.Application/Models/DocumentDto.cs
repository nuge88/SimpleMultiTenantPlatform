namespace DocumentService.Application.Documents.Models
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Size { get; set; }
        public string StoragePath { get; set; } = null!;
        public int? OrganizationId { get; set; }
        public int? UserId { get; set; }
    }
}