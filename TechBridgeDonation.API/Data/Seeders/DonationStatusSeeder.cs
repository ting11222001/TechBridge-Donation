using TechBridgeDonation.API.Models.Domain;

namespace TechBridgeDonation.API.Data.Seeders
{
    public class DonationStatusSeeder
    {
        public static async Task SeedAsync(TechBridgeDonationDbContext db)
        {
            if (db.DonationStatuses.Any()) return;

            var conditions = new List<DonationStatus>
            {
                new DonationStatus { Id = 1, Name = "Draft" },
                new DonationStatus { Id = 2, Name = "Submitted" },
                new DonationStatus { Id = 3, Name = "Approved" },
                new DonationStatus { Id = 4, Name = "Rejected" }
            };

            await db.DonationStatuses.AddRangeAsync(conditions);
            await db.SaveChangesAsync();
        }
    }
}