using System;

namespace AssetManagerPro.Models
{
    public class Asset
    {
        public int Id { get; set; }

        public string AssetCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int BrandId { get; set; }

        public int CategoryId { get; set; }

        public int? SupplierId { get; set; }

        public string? Model { get; set; }

        public string? SerialNumber { get; set; }

        public int LocationId { get; set; }

        public int? ReceiverId { get; set; }

        public int StatusId { get; set; }

        public double Price { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public DateTime? WarrantyEndDate { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
