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

        public Task<Device?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Device>> GetAllAsync()
        {
            //return await dbContext.Devices.ToListAsync();
            return await dbContext.Devices.Include("DeviceCondition").Include("DeviceStatus").ToListAsync();
        }


        public async Task<Device?> GetByIdAsync(Guid id)
        {
            return await dbContext.Devices.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Device?> UpdateAsync(Guid id, Device device)
        {
            throw new NotImplementedException();
        }
    }
}
