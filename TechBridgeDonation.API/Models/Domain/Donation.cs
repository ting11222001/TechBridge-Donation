namespace TechBridgeDonation.API.Models.Domain
{
    public class Donation
    {
        public Guid Id { get; set; }

        // FK → Organisation (donor company)
        public Guid OrganisationId { get; set; }
        public Organisation Organisation { get; set; }

        // FK → DonationStatus table
        public int DonationStatusId { get; set; }
        public DonationStatus Status { get; set; }

        public string? Notes { get; set; }
        public DateTime? SubmittedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation property
        public ICollection<Device> Devices { get; set; }
    }

    /**
     *  Draft,
        Submitted,
        Approved,
        Rejected
     */
    public class DonationStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}