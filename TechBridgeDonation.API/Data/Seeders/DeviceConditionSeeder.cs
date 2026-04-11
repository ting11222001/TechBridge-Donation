using TechBridgeDonation.API.Models.Domain;

namespace TechBridgeDonation.API.Data.Seeders
{
    public static class DeviceConditionSeeder
    {
        public static async Task SeedAsync(TechBridgeDonationDbContext db)
        {
            if (db.DeviceConditions.Any()) return;

            var conditions = new List<DeviceCondition>
            {
                new DeviceCondition { Id = 1, Name = "Good" },
                new DeviceCondition { Id = 2, Name = "Fair" },
                new DeviceCondition { Id = 3, Name = "Poor" }
            };

            await db.DeviceConditions.AddRangeAsync(conditions);
            await db.SaveChangesAsync();
        }
    }
}