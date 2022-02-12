using System.ComponentModel.DataAnnotations;

namespace Customer.Contact.Core.Dtos
{
    public class ContactRequestDto
    {       

        [RegularExpression("^[0-9]{10,12}$")]
        public long SocialSecurityNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^([+]46)[0-9]{9}$")]
        public string? PhoneNumber { get; set; }
    }
}
