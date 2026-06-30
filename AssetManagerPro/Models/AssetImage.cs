namespace AssetManagerPro.Models
{
    public class AssetImage
    {
        public int Id { get; set; }

        public int AssetId { get; set; }

        public string ImagePath { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}