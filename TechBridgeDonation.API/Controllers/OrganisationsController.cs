using Microsoft.AspNetCore.Mvc;
using TechBridgeDonation.API.Data;
using TechBridgeDonation.API.Models.Domain;
using TechBridgeDonation.API.Models.DTO;
using TechBridgeDonation.API.Repositories;

namespace TechBridgeDonation.API.Controllers
{
    // https://localhost:5001/api/organisations
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationsController : ControllerBase
    {
        private readonly TechBridgeDonationDbContext dbContext;
        private readonly IOrganisationRepository organisationRepository;

        public OrganisationsController(TechBridgeDonationDbContext dbContext, IOrganisationRepository organisationRepository)
        {
            this.dbContext = dbContext;
            this.organisationRepository = organisationRepository;
        }

        // GET ALL ORGs
        // GET: https://localhost:portnumber/api/organisations
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get data from database - Org models
            var organisationDomain = await organisationRepository.GetAllAsync();

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
            var organisationDomain = await organisationRepository.GetByIdAsync(id); // x is just a name for each row as EF loops through. x.Id == id is the filter.

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
            organisationDomainModel = await organisationRepository.CreateAsync(organisationDomainModel);

            // Map Domain Model back to DTO to send the result back to frontend
            var organisationDto = new OrganisationDto
            {
                Id = organisationDomainModel.Id,
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
            // Map DTO to Domain Model
            var updateOrganisationDomainModel = new Organisation
            {
                Name = updateOrganisationRequestDto.Name,
                Type = updateOrganisationRequestDto.Type,
                ContactEmail = updateOrganisationRequestDto.ContactEmail,
                ContactPhone = updateOrganisationRequestDto.ContactPhone,
                Address = updateOrganisationRequestDto.Address,
            };

            var updatedOrganisationDomainModel = await organisationRepository.UpdateAsync(id, updateOrganisationDomainModel);

            if (updatedOrganisationDomainModel == null)
            {
                return NotFound();
            }

            // Return the updated Organistaion back to client
            // Map Domain Model to DTO
            var organisationDto = new OrganisationDto
            {
                Id = updatedOrganisationDomainModel.Id,
                Name = updatedOrganisationDomainModel.Name,
                Type = updatedOrganisationDomainModel.Type,
                ContactEmail = updatedOrganisationDomainModel.ContactEmail,
                ContactPhone = updatedOrganisationDomainModel.ContactPhone,
                Address = updatedOrganisationDomainModel.Address,
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
            var deletedOrganisationDomainModel = await organisationRepository.DeleteAsync(id);

            if (deletedOrganisationDomainModel == null)
            {
                return NotFound();
            }

            // Return the deleted Organistaion back to client
            // Map Domain Model to DTO
            var organisationDto = new OrganisationDto
            {
                Id = deletedOrganisationDomainModel.Id,
                Name = deletedOrganisationDomainModel.Name,
                Type = deletedOrganisationDomainModel.Type,
                ContactEmail = deletedOrganisationDomainModel.ContactEmail,
                ContactPhone = deletedOrganisationDomainModel.ContactPhone,
                Address = deletedOrganisationDomainModel.Address,
            };

            return Ok(organisationDto);
        }
    }
}
