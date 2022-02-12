using Customer.Contact.Core.Dtos;
using Customer.Contact.Core.Interfaces;

namespace Customer.Contact.Core.Services
{
    public class ContactService : IContactService
    {
        private static readonly string UserIdForAuditTrail = $"{Environment.UserName}@{Environment.MachineName}".ToUpperInvariant();
        private readonly IContactReository _contactReository;        

        public ContactService(IContactReository contactReository)
        {
            _contactReository = contactReository;            
        }

        /// <inheritdoc/>
        public async Task<int> CreateContact(ContactRequestDto contactDetails)
        {
            var contactEntity = new Entities.Contact
            {
                Email = contactDetails.Email,
                SocialSecurityNumber = contactDetails.SocialSecurityNumber,
                PhoneNumber = contactDetails.PhoneNumber,
                CreatedOn = DateTime.Now,
                CreatedBy = UserIdForAuditTrail,
            };

            var contact = await _contactReository.CreateContact(contactEntity);
           
            return contact.Id;
        }

        /// <inheritdoc/>
        public async Task DeleteContactDetails(int id)
        {
            await _contactReository.DeleteContactDetails(id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ContactResponseDto>> GetAllContactDetails()
        {
            var contactDto = new List<ContactResponseDto>();
            var contactDetail = await _contactReository.GetAllContactDetails();
            if(contactDetail.Count() > 0)
            {
                foreach (var contact in contactDetail)
                {
                    contactDto.Add(new ContactResponseDto
                    {
                        Email = contact.Email,
                        SocialSecurityNumber = contact.SocialSecurityNumber,
                        PhoneNumber = contact.PhoneNumber,
                        CustomerId = contact.Id
                    });
                }
            }
            return contactDto;
        }

        /// <inheritdoc/>
        public async Task<ContactResponseDto?> GetContactDetailsById(int id)
        {
            var details = await _contactReository.GetContactDetailsById(id);
            if (details == null)
            {                
                return null;
            }
            
            return new ContactResponseDto
            {                
                Email = details.Email,
                PhoneNumber = details.PhoneNumber,
                SocialSecurityNumber = details.SocialSecurityNumber,
                CustomerId = details.Id
            };
        }

        /// <inheritdoc/>
        public async Task UpdateContactDetails(ContactRequestDto contactDetails, int customerId)
        {
            var contactEntity = new Entities.Contact
            {
                Id = customerId,
                Email = contactDetails.Email,
                SocialSecurityNumber = contactDetails.SocialSecurityNumber,
                PhoneNumber = contactDetails.PhoneNumber,
                UpdatedOn = DateTime.Now,
                UpdatedBy = UserIdForAuditTrail,
            };
            await _contactReository.UpdateContactDetails(contactEntity);
        }
    }
}
