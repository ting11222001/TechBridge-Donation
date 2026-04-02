using Microsoft.AspNetCore.Mvc;
using TechBridgeDonation.API.Data;

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
            var orgs = dbContext.Organisations.ToList();

            return Ok(orgs);
        }

        // GET SINGLE ORGs BY ID
        // GET: https://localhost:portnumber/api/organisations/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var orgs = dbContext.Organisations.FirstOrDefault(x => x.Id == id); // x is just a name for each row as EF loops through. x.Id == id is the filter.

            if (orgs == null) // If no match is found, it returns null instead of throwing an error. 
            {
                return NotFound();
            }

            return Ok(orgs);
        }
    }
}
