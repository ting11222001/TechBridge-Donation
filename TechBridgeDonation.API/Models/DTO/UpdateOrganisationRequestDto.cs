namespace TechBridgeDonation.API.Models.DTO
{
    public class UpdateOrganisationRequestDto
    {
        public string Name { get; set; }
        public int OrganisationTypeId { get; set; }
        public string ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
    }
}
