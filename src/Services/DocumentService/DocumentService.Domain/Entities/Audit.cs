namespace DocumentService.Domain.Entities
{
    public class Audit
    {
        public int Id { get; set; } // Primary key
        public int EntityId { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public int UserId { get; set; }

        /// <summary>
        ///  E.g., Create, Update, Delete
        /// </summary>
        public string Action { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}