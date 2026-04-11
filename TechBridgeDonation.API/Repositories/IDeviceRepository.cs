using TechBridgeDonation.API.Models.Domain;

namespace TechBridgeDonation.API.Repositories
{
    public interface IDeviceRepository
    {
        Task<List<Device>> GetAllAsync();

        Task<Device?> GetByIdAsync(Guid id);

        Task<Device> CreateAsync(Device device);

        Task<Device?> UpdateAsync(Guid id, Device device);

        Task<Device?> DeleteAsync(Guid id);
    }
}
