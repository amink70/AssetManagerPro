using System;

namespace AssetManagerPro.Models
{
    public class Maintenance
    {
        public int Id { get; set; }

        public int AssetId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public double Cost { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Status { get; set; }

        public string? Description { get; set; }
    }
}