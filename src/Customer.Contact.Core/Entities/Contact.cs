namespace Customer.Contact.Core.Entities
{
    public partial class Contact
    {
        public int Id { get; set; }
        public long SocialSecurityNumber { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
