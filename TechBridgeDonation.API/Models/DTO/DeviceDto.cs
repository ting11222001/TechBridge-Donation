namespace TechBridgeDonation.API.Models.DTO
{
    public class DeviceDto
    {
        public Guid Id { get; set; }
        public Guid DonationId { get; set; }
        public string DeviceType { get; set; }   // e.g. laptop, tablet
        public string Brand { get; set; }
        public string Model { get; set; }
        public int DeviceConditionId { get; set; } // this will return just the id
        public DeviceConditionDto DeviceCondition { get; set; } // so that this deetail of the condition will show up in the response
        public int DeviceStatusId { get; set; }

        public DeviceStatusDto DeviceStatus { get; set; } // so that this will show up in the response
        public Guid? AssignedRefurbPartnerId { get; set; }
        public string? TechnicalNotes { get; set; }
        public bool WipeCompleted { get; set; }
        public bool RefurbCompleted { get; set; }
        public DateTime? StatusChangedAt { get; set; }
        public Guid? StatusChangedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }  // soft delete
    }
}
