using System;
using AssetManagerPro.Enums;

namespace AssetManagerPro.Models
{
    public class AssetTransaction
    {
        public int Id { get; set; }

        public int AssetId { get; set; }

        public int? ReceiverId { get; set; }

        public int LocationId { get; set; }

        public int UserId { get; set; }

        public TransactionType TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }

        public string? Description { get; set; }
    }
}

