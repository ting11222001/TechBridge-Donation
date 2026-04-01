namespace TechBridgeDonation.API.Models.Domain
{
    public class Donation
    {
        public Guid Id { get; set; }

        // FK → Organisation (donor company)
        public Guid OrganisationId { get; set; }
        public Organisation Organisation { get; set; }

        public DonationStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime? SubmittedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation property
        public ICollection<Device> Devices { get; set; }
    }

    public enum DonationStatus
    {
        Draft,
        Submitted,
        Approved,
        Rejected
    }
}