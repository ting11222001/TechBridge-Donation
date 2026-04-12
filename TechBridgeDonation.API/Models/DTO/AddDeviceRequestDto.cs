using System.ComponentModel.DataAnnotations;

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
        [Required]
        public Guid DonationId { get; set; }         // FK → Donation table

        [Required]
        [MinLength(3, ErrorMessage = "Device Type has to be a minimum of 3 characters")]
        [MaxLength(100, ErrorMessage = "Device Type has to be a maximum of 100 characters")]
        public string DeviceType { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Brand has to be a minimum of 3 characters")]
        [MaxLength(100, ErrorMessage = "Brand has to be a maximum of 100 characters")]
        public string Brand { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Model has to be a minimum of 3 characters")]
        [MaxLength(100, ErrorMessage = "Model has to be a maximum of 100 characters")]
        public string Model { get; set; }

        [Required]
        public int DeviceConditionId { get; set; }   // FK → DeviceCondition table

        [Required]
        public int DeviceStatusId { get; set; }      // FK → DeviceStatus table
    }
}
