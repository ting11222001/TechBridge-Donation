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
    }
}
