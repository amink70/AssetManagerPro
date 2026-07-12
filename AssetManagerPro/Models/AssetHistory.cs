namespace AssetManagerPro.Models
{
    public class AssetHistory
    {
        public DateTime TransactionDate { get; set; }

        public string TransactionType { get; set; } = "";

        public string ReceiverName { get; set; } = "";

        public string LocationName { get; set; } = "";

        public string UserName { get; set; } = "";

        public string Description { get; set; } = "";
    }
}