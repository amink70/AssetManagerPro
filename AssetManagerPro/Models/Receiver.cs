namespace AssetManagerPro.Models
{
    public class Receiver
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string PersonnelCode { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public bool IsActive { get; set; }
    }
}