using System.ComponentModel.DataAnnotations;

namespace TechBridgeDonation.API.Models.DTO
{
    public class UpdateDeviceRequestDto
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
