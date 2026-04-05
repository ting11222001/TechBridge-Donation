using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechBridgeDonation.API.Data;
using TechBridgeDonation.API.Models.Domain;
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
        public async Task<IActionResult> GetAll()
        {
            // Get data from database - Org models
            var organisationDomain = await dbContext.Organisations.ToListAsync();

            // Map domain models to DTOs
            var organisastionsDTO = new List<OrganisationDto>();

            foreach (var organisation in organisationDomain)
            {
                organisastionsDTO.Add(new OrganisationDto()
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
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var organisationDomain = await dbContext.Organisations.FirstOrDefaultAsync(x => x.Id == id); // x is just a name for each row as EF loops through. x.Id == id is the filter.

            if (organisationDomain == null) // If no match is found, it returns null instead of throwing an error. 
            {
                return NotFound();
            }

            // Map domain models to DTOs
            var organisationDTO = new OrganisationDto()
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
            return Ok(organisationDTO);
        }

        // POST to Create New Organisation
        // POST: https://localhost:portnumber/api/organisations
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddOrganisationRequestDto addOrganisationRequestDto)
        {
            // Map or Convert DTO to Domain Model
            var organisationDomainModel = new Organisation
            {
                Name = addOrganisationRequestDto.Name,
                Type = addOrganisationRequestDto.Type,
                ContactEmail = addOrganisationRequestDto.ContactEmail,
                ContactPhone = addOrganisationRequestDto.ContactPhone,
                Address = addOrganisationRequestDto.Address,
            };

            // Use Domain Model to create Organisation
            await dbContext.Organisations.AddAsync(organisationDomainModel);
            await dbContext.SaveChangesAsync();


            // Map Domain Model back to DTO to send the result back to frontend
            var organisationDto = new OrganisationDto
            {
                Name = organisationDomainModel.Name,
                Type = organisationDomainModel.Type,
                ContactEmail = organisationDomainModel.ContactEmail,
                ContactPhone = organisationDomainModel.ContactPhone,
                Address = organisationDomainModel.Address,
            };


            return CreatedAtAction(nameof(GetById), new { id = organisationDto.Id }, organisationDto);
        }

        // PUT to Update Organisation
        // PUT: https://localhost:portnumber/api/organisations/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateOrganisationRequestDto updateOrganisationRequestDto)
        {
            // Check if Organisation exists
            var organisationDomainModel = await dbContext.Organisations.FirstOrDefaultAsync(x => x.Id == id);

            if (organisationDomainModel == null) // If no match is found, it returns null instead of throwing an error. 
            {
                return NotFound();
            }

            // Map or Convert DTO to Domain Model
            organisationDomainModel.Name = updateOrganisationRequestDto.Name;
            organisationDomainModel.Type = updateOrganisationRequestDto.Type;
            organisationDomainModel.ContactEmail = updateOrganisationRequestDto.ContactEmail;
            organisationDomainModel.ContactPhone = updateOrganisationRequestDto.ContactPhone;
            organisationDomainModel.Address = updateOrganisationRequestDto.Address;


            await dbContext.SaveChangesAsync();


            // Map Domain Model back to DTO to send the result back to frontend
            var organisationDto = new OrganisationDto
            {
                Name = organisationDomainModel.Name,
                Type = organisationDomainModel.Type,
                ContactEmail = organisationDomainModel.ContactEmail,
                ContactPhone = organisationDomainModel.ContactPhone,
                Address = organisationDomainModel.Address,
            };


            // Return DTOs
            return Ok(organisationDto);
        }

        // Delete one Organisation
        // DELETE: https://localhost:portnumber/api/organisations/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Check if Organisation exists
            var organisationDomainModel = await dbContext.Organisations.FirstOrDefaultAsync(x => x.Id == id);

            if (organisationDomainModel == null) // If no match is found, it returns null instead of throwing an error. 
            {
                return NotFound();
            }

            // Delete the Organisation
            dbContext.Organisations.Remove(organisationDomainModel);
            await dbContext.SaveChangesAsync();

            // Return delete Organistaion back to client
            // Map Organisation Model to DTO
            var organisationDto = new OrganisationDto
            {
                Name = organisationDomainModel.Name,
                Type = organisationDomainModel.Type,
                ContactEmail = organisationDomainModel.ContactEmail,
                ContactPhone = organisationDomainModel.ContactPhone,
                Address = organisationDomainModel.Address,
            };

            return Ok(organisationDto);
        }
    }
}
