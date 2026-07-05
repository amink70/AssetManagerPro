namespace AssetManagerPro.Models
{
    public class Status : ILookupEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Color { get; set; }

        public string? Description { get; set; }
    }
}