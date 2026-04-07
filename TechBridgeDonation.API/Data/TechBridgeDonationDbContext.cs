using Microsoft.EntityFrameworkCore;
using TechBridgeDonation.API.Models.Domain;

namespace TechBridgeDonation.API.Data
{
    public class TechBridgeDonationDbContext : DbContext
    {
        public TechBridgeDonationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Donation> Donations { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
    }
}
