using System;

namespace AssetManagerPro.Models
{
    public class AssetDisplay
    {
        public int Id { get; set; }

        public string AssetCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string SerialNumber { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Receiver { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public double Price { get; set; }

        public DateTime? PurchaseDate { get; set; }
    }
}