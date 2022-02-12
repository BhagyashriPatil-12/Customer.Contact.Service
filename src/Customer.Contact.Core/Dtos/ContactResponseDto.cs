namespace Customer.Contact.Core.Dtos
{
    public class ContactResponseDto
    {
        public int CustomerId { get; set; }

        public long SocialSecurityNumber { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
