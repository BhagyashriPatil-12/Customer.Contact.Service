using Customer.Contact.Core.Dtos;

namespace Customer.Contact.Core.Interfaces
{
    public interface IContactService
    {
        /// <summary>
        /// Create customer contact details
        /// </summary>
        /// <returns>Id of customer contact</returns>
        Task<int> CreateContact(ContactRequestDto contactDetails);

        /// <summary>
        /// Get cotact details by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Contact details of customer</returns>
        Task<ContactResponseDto?> GetContactDetailsById(int id);

        /// <summary>
        /// Get all contact details
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Contact details of customer</returns>
        Task<IEnumerable<ContactResponseDto>> GetAllContactDetails();

        /// <summary>
        /// Update contact details
        /// </summary>
        /// <param name="contactDetails"></param>       
        Task UpdateContactDetails(ContactRequestDto contactDetails, int customerId);

        /// <summary>
        /// Delete contact details
        /// </summary>
        /// <param name="id"></param>       
        Task DeleteContactDetails(int id);
    }
}
