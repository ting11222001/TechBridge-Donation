using TechBridgeDonation.API.Models.Domain;

namespace TechBridgeDonation.API.Models.DTO
{
    public class AddOrganisationRequestDto
    {
        public string Name { get; set; }
        public OrganisationType Type { get; set; }
        public string ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
    }
}
