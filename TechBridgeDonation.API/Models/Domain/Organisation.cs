namespace TechBridgeDonation.API.Models.Domain
{
    public class Organisation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // FK → OrganisationType table
        public int OrganisationTypeId { get; set; }
        public OrganisationType Type { get; set; }
        public string ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }  // soft delete

        // Navigation properties
        public ICollection<Donation> Donations { get; set; }        // donations made by this org (donor business): "one organisation has many donations"
        public ICollection<Device> AssignedDevices { get; set; }    // devices assigned to this org (refurb partner): "one device can be assigned to one refurb partner" relationship
    }

    /**
     *  BusinessDonor,
        RefurbPartner,
        RequestPartner
     */
    public class OrganisationType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}