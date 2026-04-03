using Microsoft.AspNetCore.Mvc;
using TechBridgeDonation.API.Data;
using TechBridgeDonation.API.Models.DTO;

namespace TechBridgeDonation.API.Controllers
{
    // https://localhost:5001/api/organisations
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationsController : ControllerBase
    {
        private readonly TechBridgeDonationDbContext dbContext;

        public OrganisationsController(TechBridgeDonationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL ORGs
        // GET: https://localhost:portnumber/api/organisations
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get data from database - Org models
            var organisationDomain = dbContext.Organisations.ToList();

            // Map domain models to DTOs
            var organisastionsDTO = new List<OrganisationDTO>();

            foreach (var organisation in organisationDomain)
            {
                organisastionsDTO.Add(new OrganisationDTO()
                {
                    Id = organisation.Id,
                    Name = organisation.Name,
                    Type = organisation.Type,
                    ContactEmail = organisation.ContactEmail,
                    ContactPhone = organisation.ContactPhone,
                    Address = organisation.Address,
                    CreatedAt = organisation.CreatedAt,
                    UpdatedAt = organisation.UpdatedAt,
                    DeletedAt = organisation.DeletedAt
                });
            }

            // Return DTOs
            return Ok(organisastionsDTO);
        }

        // GET SINGLE ORGs BY ID
        // GET: https://localhost:portnumber/api/organisations/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var organisationDomain = dbContext.Organisations.FirstOrDefault(x => x.Id == id); // x is just a name for each row as EF loops through. x.Id == id is the filter.

            if (organisationDomain == null) // If no match is found, it returns null instead of throwing an error. 
            {
                return NotFound();
            }

            // Map domain models to DTOs
            var organisastionDTO = new OrganisationDTO()
            {
                Id = organisationDomain.Id,
                Name = organisationDomain.Name,
                Type = organisationDomain.Type,
                ContactEmail = organisationDomain.ContactEmail,
                ContactPhone = organisationDomain.ContactPhone,
                Address = organisationDomain.Address,
                CreatedAt = organisationDomain.CreatedAt,
                UpdatedAt = organisationDomain.UpdatedAt,
                DeletedAt = organisationDomain.DeletedAt
            };

            // Return DTOs
            return Ok(organisastionDTO);
        }
    }
}
