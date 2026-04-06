using Microsoft.EntityFrameworkCore;
using TechBridgeDonation.API.Data;
using TechBridgeDonation.API.Models.Domain;

namespace TechBridgeDonation.API.Repositories
{
    public class SQLOrganisationRepository : IOrganisationRepository
    {
        private readonly TechBridgeDonationDbContext dbContext;
        public SQLOrganisationRepository(TechBridgeDonationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Organisation>> GetAllAsync()
        {
            return await dbContext.Organisations.ToListAsync();
        }

        public async Task<Organisation?> GetByIdAsync(Guid id)
        {
            return await dbContext.Organisations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Organisation> CreateAsync(Organisation organisation)
        {
            await dbContext.Organisations.AddAsync(organisation);
            await dbContext.SaveChangesAsync();
            return organisation;
        }

        public async Task<Organisation?> UpdateAsync(Guid id, Organisation organisation)
        {
            // Check if Organisation exists
            var existingOrganisationDomainModel = await dbContext.Organisations.FirstOrDefaultAsync(x => x.Id == id);

            if (existingOrganisationDomainModel == null) // If no match is found, it returns null instead of throwing an error. 
            {
                return null;
            }

            // Map or Convert DTO to Domain Model
            existingOrganisationDomainModel.Name = organisation.Name;
            existingOrganisationDomainModel.Type = organisation.Type;
            existingOrganisationDomainModel.ContactEmail = organisation.ContactEmail;
            existingOrganisationDomainModel.ContactPhone = organisation.ContactPhone;
            existingOrganisationDomainModel.Address = organisation.Address;


            await dbContext.SaveChangesAsync();

            return existingOrganisationDomainModel;
        }

        public async Task<Organisation?> DeleteAsync(Guid id)
        {
            // Check if Organisation exists
            var existingOrganisationDomainModel = await dbContext.Organisations.FirstOrDefaultAsync(x => x.Id == id);

            if (existingOrganisationDomainModel == null) // If no match is found, it returns null instead of throwing an error. 
            {
                return null;
            }

            // Delete the Organisation
            dbContext.Organisations.Remove(existingOrganisationDomainModel);
            await dbContext.SaveChangesAsync();

            return existingOrganisationDomainModel;
        }
    }
}
