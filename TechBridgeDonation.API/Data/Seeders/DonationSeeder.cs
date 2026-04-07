using TechBridgeDonation.API.Models.Domain;

namespace TechBridgeDonation.API.Data.Seeders
{
    public static class DonationSeeder
    {
        public static readonly Guid SubmittedDonationId = Guid.Parse("eb49109e-b53d-4625-be17-9c624162c467");
        public static readonly Guid DraftDonationId = Guid.Parse("eb49109e-b53d-4638-be17-9c624162c467");

        // Accept the org ID so we can link to it
        public static async Task SeedAsync(TechBridgeDonationDbContext db, Guid orgId)
        {
            if (db.Donations.Any()) return;

            var donations = new List<Donation>
            {
                new Donation
                {
                    Id = SubmittedDonationId,
                    OrganisationId = orgId,
                    Status = DonationStatus.Submitted,
                    Notes = "First batch of laptops from TechCorp",
                    SubmittedAt = new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2026, 4, 2, 0, 0, 0, DateTimeKind.Utc)
                },
                new Donation
                {
                    Id = DraftDonationId,
                    OrganisationId = orgId,
                    Status = DonationStatus.Draft,
                    Notes = null,
                    SubmittedAt = null,
                    CreatedAt = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2026, 4, 3, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            await db.Donations.AddRangeAsync(donations);
            await db.SaveChangesAsync();
        }
    }
}
