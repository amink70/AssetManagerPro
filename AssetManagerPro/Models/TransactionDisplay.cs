namespace AssetManagerPro.Models
{
    public class TransactionDisplay
    {
        public int Id { get; set; }

        public string AssetName { get; set; } = "";

        public string ReceiverName { get; set; } = "";

        public string LocationName { get; set; } = "";

        public string TransactionType { get; set; } = "";

        public DateTime TransactionDate { get; set; }

        public string Description { get; set; } = "";
    }
}
