using Microsoft.EntityFrameworkCore;
using TechBridgeDonation.API.Data;
using TechBridgeDonation.API.Models.Domain;

namespace TechBridgeDonation.API.Repositories
{
    public class SQLDeviceRepository : IDeviceRepository
    {
        private readonly TechBridgeDonationDbContext dbContext;
        public SQLDeviceRepository(TechBridgeDonationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Device> CreateAsync(Device device)
        {
            await dbContext.Devices.AddAsync(device);
            await dbContext.SaveChangesAsync();
            return device;
        }

        public async Task<Device?> DeleteAsync(Guid id)
        {
            // Check if Device exists
            var existingDeviceDomainModel = await dbContext.Devices.FirstOrDefaultAsync(x => x.Id == id);

            if (existingDeviceDomainModel == null) // If no match is found, it returns null instead of throwing an error. 
            {
                return null;
            }

            // Delete the Organisation
            dbContext.Devices.Remove(existingDeviceDomainModel);
            await dbContext.SaveChangesAsync();

            return existingDeviceDomainModel;
        }

        public async Task<List<Device>> GetAllAsync()
        {
            //return await dbContext.Devices.ToListAsync();
            return await dbContext.Devices
                .Include("DeviceCondition")
                .Include("DeviceStatus")
                .ToListAsync();
        }


        public async Task<Device?> GetByIdAsync(Guid id)
        {
            return await dbContext.Devices
                .Include("DeviceCondition")
                .Include("DeviceStatus")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Device?> UpdateAsync(Guid id, Device device)
        {
            // Check if Device exists
            var existingDeviceDomainModel = await dbContext.Devices
                .Include("DeviceCondition")
                .Include("DeviceStatus")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingDeviceDomainModel == null)
            {
                return null;
            }

            // Map or Convert DTO to Domain Model
            existingDeviceDomainModel.DonationId = device.DonationId;
            existingDeviceDomainModel.DeviceType = device.DeviceType;
            existingDeviceDomainModel.Brand = device.Brand;
            existingDeviceDomainModel.Model = device.Model;
            existingDeviceDomainModel.DeviceConditionId = device.DeviceConditionId;
            existingDeviceDomainModel.DeviceStatusId = device.DeviceStatusId;

            await dbContext.SaveChangesAsync();

            return existingDeviceDomainModel;
        }
    }
}
