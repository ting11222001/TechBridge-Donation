using System.ComponentModel.DataAnnotations;

namespace TechBridgeDonation.API.Models.DTO
{
    public class AddOrganisationRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name has to be a minimum of 3 characters")]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }

        [Required]
        public int OrganisationTypeId { get; set; }

        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
    }
}
