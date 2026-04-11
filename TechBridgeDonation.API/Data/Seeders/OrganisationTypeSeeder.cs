using TechBridgeDonation.API.Models.Domain;

namespace TechBridgeDonation.API.Data.Seeders
{
    public class OrganisationTypeSeeder
    {
        public static async Task SeedAsync(TechBridgeDonationDbContext db)
        {
            if (db.OrganisationTypes.Any()) return;

            var conditions = new List<OrganisationType>
            {
                new OrganisationType { Id = 1, Name = "BusinessDonor" },
                new OrganisationType { Id = 2, Name = "RefurbPartner" },
                new OrganisationType { Id = 3, Name = "RequestPartner" }
            };

            await db.OrganisationTypes.AddRangeAsync(conditions);
            await db.SaveChangesAsync();
        }
    }
}
