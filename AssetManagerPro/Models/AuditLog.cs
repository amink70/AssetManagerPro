using System;

namespace AssetManagerPro.Models
{
    public class AuditLog
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Action { get; set; } = string.Empty;

        public string TableName { get; set; } = string.Empty;

        public int? RecordId { get; set; }

        public DateTime ActionDate { get; set; }

        public string? Description { get; set; }
    }
}