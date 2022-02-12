using Customer.Contact.Core.Entities;
using Customer.Contact.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Customer.Contact.Infrastructure.Repository
{
    public class ContactRepository : IContactReository
    {
        private readonly AppDbContext _appDbContext;

        public ContactRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext)); ;
        }

        /// <inheritdoc/>
        public async Task<Core.Entities.Contact> CreateContact(Core.Entities.Contact contactDetails)
        {
            await _appDbContext.Contact.AddAsync(contactDetails);
            await _appDbContext.SaveChangesAsync();
            return contactDetails;
        }

        /// <inheritdoc/>
        public async Task DeleteContactDetails(int id)
        {
            var contactDetails = await _appDbContext.Contact.FindAsync(id);
            _appDbContext.Remove(contactDetails);
            await _appDbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Core.Entities.Contact>> GetAllContactDetails()
        {
            return await _appDbContext.Contact.AsNoTracking().ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Core.Entities.Contact> GetContactDetailsById(int id)
        {            
            return await _appDbContext.Contact.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        }

        /// <inheritdoc/>
        public async Task UpdateContactDetails(Core.Entities.Contact contactDetails)
        {
            _appDbContext.Update(contactDetails);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
