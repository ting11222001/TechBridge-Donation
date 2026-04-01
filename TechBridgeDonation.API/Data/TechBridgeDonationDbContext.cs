using Microsoft.EntityFrameworkCore;

namespace TechBridgeDonation.API.Data
{
    public class TechBridgeDonationDbContext: DbContext
    {
        public TechBridgeDonationDbContext(DbContextOptions dbContextOptions): base(dbContextOptions){}

        public DbSet<Models.Donation> Donations { get; set; }
        public DbSet<Models.Device> Devices { get; set; }
        public DbSet<Models.Organisation> Organisations { get; set; }
    }
}
