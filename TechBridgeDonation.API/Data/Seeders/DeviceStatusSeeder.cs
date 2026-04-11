using TechBridgeDonation.API.Models.Domain;

namespace TechBridgeDonation.API.Data.Seeders
{
    public static class DeviceStatusSeeder
    {
        public static async Task SeedAsync(TechBridgeDonationDbContext db)
        {
            if (db.DeviceStatuses.Any()) return;

            var statuses = new List<DeviceStatus>
            {
                new DeviceStatus { Id = 1,  Name = "Draft" },
                new DeviceStatus { Id = 2,  Name = "Submitted" },
                new DeviceStatus { Id = 3,  Name = "Approved" },
                new DeviceStatus { Id = 4,  Name = "AssignedForWipe" },
                new DeviceStatus { Id = 5,  Name = "Wiped" },
                new DeviceStatus { Id = 6,  Name = "Refurbished" },
                new DeviceStatus { Id = 7,  Name = "ReadyForAllocation" },
                new DeviceStatus { Id = 8,  Name = "Allocated" },
                new DeviceStatus { Id = 9,  Name = "Delivered" },
                new DeviceStatus { Id = 10, Name = "Closed" },
                new DeviceStatus { Id = 11, Name = "Rejected" }
            };

            await db.DeviceStatuses.AddRangeAsync(statuses);
            await db.SaveChangesAsync();
        }
    }
}