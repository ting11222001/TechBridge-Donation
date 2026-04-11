namespace TechBridgeDonation.API.Models.DTO
{
    /**
     * Plan:
        Include: DonationId, DeviceType, Brand, Model, Condition
        
        Exclude everything else because:
        Id, CreatedAt, UpdatedAt are set by the server
        AssignedRefurbPartnerId, TechnicalNotes, WipeCompleted, RefurbCompleted are set later in the workflow, not at creation
        StatusChangedAt, StatusChangedBy, DeletedAt are lifecycle fields
     */
    public class AddDeviceRequestDto
    {
        // FK → Donation table
        public Guid DonationId { get; set; }
        public string DeviceType { get; set; }   // e.g. laptop, tablet
        public string Brand { get; set; }
        public string Model { get; set; }

        // FK → DeviceCondition table
        public int DeviceConditionId { get; set; }

        // FK → DeviceStatus table
        public int DeviceStatusId { get; set; }
    }
}
