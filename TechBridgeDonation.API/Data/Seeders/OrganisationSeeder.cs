using TechBridgeDonation.API.Data;
using TechBridgeDonation.API.Models.Domain;

public static class OrganisationSeeder
{
    public static readonly Guid DonorOrgId = Guid.Parse("e56e6e2f-c2b1-4465-8978-9344841739e3");
    public static readonly Guid RefurbOrgId = Guid.Parse("1afc8681-e982-44ed-ae89-6659f1c826ff");

    public static async Task SeedAsync(TechBridgeDonationDbContext db)
    {
        if (db.Organisations.Any()) return; // If table has rows → `Any()` returns `true` → `return` early, skip seeding

        var organisations = new List<Organisation>
        {
            new Organisation
            {
                Id = DonorOrgId,
                Name = "TechCorp Pty Ltd",
                Type = OrganisationType.BusinessDonor,
                ContactEmail = "donate@techcorp.com.au",
                ContactPhone = "0412345678",
                Address = "1 King William St, Adelaide SA 5000",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Organisation
            {
                Id = RefurbOrgId,
                Name = "Refurb Adelaide",
                Type = OrganisationType.RefurbPartner,
                ContactEmail = "hello@refurbadelaide.org.au",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        };

        await db.Organisations.AddRangeAsync(organisations);
        await db.SaveChangesAsync();
    }
}