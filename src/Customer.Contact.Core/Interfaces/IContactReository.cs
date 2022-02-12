namespace Customer.Contact.Core.Interfaces
{
    public interface IContactReository
    {
        /// <summary>
        /// Ceaate customer contact detals
        /// </summary>
        /// <returns>Created contac entity details</returns>
        Task<Entities.Contact> CreateContact(Entities.Contact contactDetails);

        /// <summary>
        /// Get cotact details by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Contact details of the customer </returns>
        Task<Entities.Contact> GetContactDetailsById(int id);

        /// <summary>
        /// Get all contact details
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Contact details of customer</returns>
        Task<IEnumerable<Entities.Contact>> GetAllContactDetails();

        /// <summary>
        /// Update contact details
        /// </summary>
        /// <param name="contactDetails"></param>        
        Task UpdateContactDetails(Entities.Contact contactDetails);

        /// <summary>
        /// Delete ontact details
        /// </summary>
        /// <param name="id"></param>        
        Task DeleteContactDetails(int id);

    }
}
