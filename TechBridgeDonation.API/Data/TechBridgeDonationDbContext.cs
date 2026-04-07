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

        // Another way to seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Organisations
            var organisations = new List<Organisation>
            {
                new Organisation
                {
                    Id = Guid.NewGuid(),
                    Name = "Adelaide Electronics Pty Ltd",
                    Type = OrganisationType.RefurbPartner,
                    ContactEmail = "fix@adlElect.com.au",
                    ContactPhone = "0412345678",
                    Address = "60 King William St, Adelaide SA 5000",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            modelBuilder.Entity<Organisation>().HasData(organisations);
        }
    }
}
