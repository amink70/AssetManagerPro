namespace AssetManagerPro.Models
{
    public class Location
    {
        public int Id { get; set; }

        public int DepartmentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}