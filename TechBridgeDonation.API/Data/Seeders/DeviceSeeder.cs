// Data/Seeders/DeviceSeeder.cs
using TechBridgeDonation.API.Models.Domain;

namespace TechBridgeDonation.API.Data.Seeders
{
    public static class DeviceSeeder
    {
        public static async Task SeedAsync(TechBridgeDonationDbContext db, Guid donationId, Guid? refurbPartnerId)
        {
            if (db.Devices.Any()) return;

            var devices = new List<Device>
            {
                new Device
                {
                    Id = Guid.NewGuid(),
                    DonationId = donationId,
                    DeviceType = "Laptop",
                    Brand = "Dell",
                    Model = "Latitude 5490",
                    Condition = DeviceCondition.Good,
                    Status = DeviceStatus.Submitted,
                    AssignedPartnerId = refurbPartnerId,
                    TechnicalNotes = "Missing charger",
                    WipeCompleted = false,
                    RefurbCompleted = false,
                    CreatedAt = new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2026, 4, 2, 0, 0, 0, DateTimeKind.Utc)
                },
                new Device
                {
                    Id = Guid.NewGuid(),
                    DonationId = donationId,
                    DeviceType = "Tablet",
                    Brand = "Lenovo",
                    Model = "Tab M10",
                    Condition = DeviceCondition.Fair,
                    Status = DeviceStatus.Approved,
                    AssignedPartnerId = refurbPartnerId,
                    TechnicalNotes = null,
                    WipeCompleted = true,
                    RefurbCompleted = true,
                    CreatedAt = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2026, 4, 3, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            await db.Devices.AddRangeAsync(devices);
            await db.SaveChangesAsync();
        }
    }
}