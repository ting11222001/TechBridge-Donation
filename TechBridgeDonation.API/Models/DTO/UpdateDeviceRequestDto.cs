namespace TechBridgeDonation.API.Models.DTO
{
    public class UpdateDeviceRequestDto
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
