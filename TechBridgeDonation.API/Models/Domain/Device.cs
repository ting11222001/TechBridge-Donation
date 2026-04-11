namespace TechBridgeDonation.API.Models.Domain
{
    public class Device
    {
        public Guid Id { get; set; }

        // FK → Donation
        public Guid DonationId { get; set; }
        public Donation Donation { get; set; }

        public string DeviceType { get; set; }   // e.g. laptop, tablet
        public string Brand { get; set; }
        public string Model { get; set; }


        // FK → DeviceCondition table
        public int DeviceConditionId { get; set; }
        public DeviceCondition Condition { get; set; }


        // FK → DeviceStatus table
        public int DeviceStatusId { get; set; }
        public DeviceStatus Status { get; set; }


        // FK → Organisation (refurb partner)
        public Guid? AssignedRefurbPartnerId { get; set; }
        public Organisation AssignedRefurbPartner { get; set; }

        public string? TechnicalNotes { get; set; }
        public bool WipeCompleted { get; set; }
        public bool RefurbCompleted { get; set; }

        public DateTime? StatusChangedAt { get; set; }

        // FK → User who changed status
        public Guid? StatusChangedBy { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }  // soft delete
    }

    /**
     *  Good,
        Fair,
        Poor
     */
    public class DeviceCondition
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    /**
     *  Draft,
        Submitted,
        Approved,
        AssignedForWipe,
        Wiped,
        Refurbished,
        ReadyForAllocation,
        Allocated,
        Delivered,
        Closed,
        Rejected
     */
    public class DeviceStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}