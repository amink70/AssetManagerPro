namespace AssetManagerPro.Models
{
    public class Supplier : ILookupEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? ManagerName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? Description { get; set; }
    }
}