using Microsoft.EntityFrameworkCore;

namespace TechBridgeDonation.API.Data
{
    public class TechBridgeDonationDbContext: DbContext
    {
        public TechBridgeDonationDbContext(DbContextOptions dbContextOptions): base(dbContextOptions){}


    }
}
