namespace TechBridgeDonation.API.Models.Domain
{
    public class Organisation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public OrganisationType Type { get; set; }
        public string ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }  // soft delete

        // Navigation properties
        public ICollection<Device> AssignedDevices { get; set; }  // devices assigned to this org
    }

    public enum OrganisationType
    {
        BusinessDonor,
        RefurbPartner,
        RequestPartner
    }
}