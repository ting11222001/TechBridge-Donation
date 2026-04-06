using TechBridgeDonation.API.Models.Domain;

namespace TechBridgeDonation.API.Repositories
{
    public interface IOrganisationRepository
    {
        Task<List<Organisation>> GetAllAsync();

        Task<Organisation?> GetByIdAsync(Guid id);

        Task<Organisation> CreateAsync(Organisation organisation);

        Task<Organisation?> UpdateAsync(Guid id, Organisation organisation);

        Task<Organisation?> DeleteAsync(Guid id);
    }
}
