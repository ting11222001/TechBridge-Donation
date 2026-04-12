using AutoMapper;
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
        private readonly IMapper mapper;

        public OrganisationsController(
            TechBridgeDonationDbContext dbContext,
            IOrganisationRepository organisationRepository,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.organisationRepository = organisationRepository;
            this.mapper = mapper;
        }

        // GET ALL ORGs
        // GET: https://localhost:portnumber/api/organisations
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get a list of Organisation Domain Models from database
            var organisationsDomainModel = await organisationRepository.GetAllAsync();

            // Map domain models to DTOs (with AutoMapper)
            var organisastionsDto = mapper.Map<List<OrganisationDto>>(organisationsDomainModel);

            // Return DTOs
            return Ok(organisastionsDto);
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

            // Map domain model to DTO (with AutoMapper)
            var organisationDto = mapper.Map<OrganisationDto>(organisationDomain);

            // Return DTO
            return Ok(organisationDto);
        }

        // POST to Create New Organisation
        // POST: https://localhost:portnumber/api/organisations
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddOrganisationRequestDto addOrganisationRequestDto)
        {
            // Added Model Validation using Data Annotations in the AddOrganisationRequestDto
            if (ModelState.IsValid)
            {
                // If the model state is valid, we can proceed with creating the organisation
                // Map DTO to Domain Model (with AutoMapper)
                var organisationDomainModel = mapper.Map<Organisation>(addOrganisationRequestDto);

                // Use Domain Model to create Organisation
                organisationDomainModel = await organisationRepository.CreateAsync(organisationDomainModel);

                // Map Domain Model back to DTO to send the result back to frontend (with AutoMapper)
                var organisationDto = mapper.Map<OrganisationDto>(organisationDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = organisationDto.Id }, organisationDto);
            }
            else
            {
                // If the model state is invalid, we return a Bad Request response with the validation errors
                return BadRequest(ModelState);
            }
        }

        // PUT to Update Organisation
        // PUT: https://localhost:portnumber/api/organisations/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateOrganisationRequestDto updateOrganisationRequestDto)
        {
            // Added Model Validation using Data Annotations in the UpdateOrganisationRequestDto
            if (ModelState.IsValid)
            {
                // Map DTO to Domain Model
                var updateOrganisationDomainModel = mapper.Map<Organisation>(updateOrganisationRequestDto);

                var updatedOrganisationDomainModel = await organisationRepository.UpdateAsync(id, updateOrganisationDomainModel);

                if (updatedOrganisationDomainModel == null)
                {
                    return NotFound();
                }

                // Return the updated Organistaion back to client
                // Map Domain Model to DTO (with AutoMapper)
                var organisationDto = mapper.Map<OrganisationDto>(updatedOrganisationDomainModel);


                // Return DTOs
                return Ok(organisationDto);
            }
            else
            {
                return BadRequest(ModelState);
            }

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
            var organisationDto = mapper.Map<OrganisationDto>(deletedOrganisationDomainModel);

            return Ok(organisationDto);
        }
    }
}
