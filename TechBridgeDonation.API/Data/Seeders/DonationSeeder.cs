using TechBridgeDonation.API.Models.Domain;

namespace TechBridgeDonation.API.Data.Seeders
{
    public static class DonationSeeder
    {
        // Accept the org ID so we can link to it
        public static async Task SeedAsync(TechBridgeDonationDbContext db, Guid orgId)
        {
            if (db.Donations.Any()) return;

            var donations = new List<Donation>
            {
                new Donation
                {
                    Id = Guid.Parse("b1000000-0000-0000-0000-000000000001"),
                    OrganisationId = orgId,
                    Status = DonationStatus.Submitted,
                    Notes = "First batch of laptops from TechCorp",
                    SubmittedAt = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Donation
                {
                    Id = Guid.Parse("b1000000-0000-0000-0000-000000000002"),
                    OrganisationId = orgId,
                    Status = DonationStatus.Draft,
                    Notes = null,
                    SubmittedAt = null,
                    CreatedAt = new DateTime(2024, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 4, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            await db.Donations.AddRangeAsync(donations);
            await db.SaveChangesAsync();
        }
    }
}
