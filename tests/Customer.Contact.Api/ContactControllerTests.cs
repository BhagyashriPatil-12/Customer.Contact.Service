using Customer.Contact.Api.Controllers;
using Customer.Contact.Core.Dtos;
using Customer.Contact.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Customer.Contact.Api
{
    public class ContactControllerTests
    {
        private readonly Mock<IContactService> _mockContactService;
        private readonly Mock<ILogger<ContactController>> _mockLogger;
        public ContactControllerTests()
        {
            _mockContactService = new Mock<IContactService>();
            _mockLogger = new Mock<ILogger<ContactController>>();
        }

        [Fact]
        public async Task Get_Returns_AllCustomerContactDetails()
        {
            // Arrange            
            var contactController = new ContactController(_mockContactService.Object, _mockLogger.Object);

            IEnumerable<ContactResponseDto> contactDetails = new List<ContactResponseDto>
                {
                    new ContactResponseDto
                    {
                        Email = "Test@gmail.com",
                        PhoneNumber = "+46123456789",
                        CustomerId = 1,
                        SocialSecurityNumber = 1234567890,
                    },
                };
            _mockContactService.Setup(x => x.GetAllContactDetails()).Returns(Task.FromResult(contactDetails));

            // Act
            var response = await contactController.Get() as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(response);
            var items = Assert.IsType<List<ContactResponseDto>>(response?.Value);
            Assert.Single(items);

        }

        [Fact]
        public async Task Get_Returns_CustomerContactDetailsById()
        {
            // Arrange            
            var contactController = new ContactController(_mockContactService.Object, _mockLogger.Object);

            var contactDetails = new ContactResponseDto
            {
                Email = "Test@gmail.com",
                PhoneNumber = "+46123456789",
                CustomerId = 1,
                SocialSecurityNumber = 1234567890,
            };
            _mockContactService.Setup(x => x.GetContactDetailsById(It.IsAny<int>())).Returns(Task.FromResult(contactDetails));

            // Act
            var response = await contactController.Get(It.IsAny<int>()) as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task Post_Returns_CreatedContactIdAsResponse()
        {
            // Arrange            
            var contactController = new ContactController(_mockContactService.Object, _mockLogger.Object);

            var contactDetails = new ContactRequestDto
            {
                Email = "Test@gmail.com",
                PhoneNumber = "+46123456789",
                SocialSecurityNumber = 1234567890,
            };
            _mockContactService.Setup(x => x.CreateContact(contactDetails)).Returns(Task.FromResult(It.IsAny<int>()));

            // Act
            var response = await contactController.Post(contactDetails) as CreatedAtActionResult;

            // Assert
            Assert.IsType<CreatedAtActionResult>(response);
        }

        [Fact]
        public async Task Delete_Returns_TrueIfContatDetailsForGivenIdIsRemoved()
        {
            // Arrange           
            var contactController = new ContactController(_mockContactService.Object, _mockLogger.Object);
            _mockContactService.Setup(x => x.DeleteContactDetails(It.IsAny<int>())).Returns(Task.FromResult(true));

            // Act
            var response = await contactController.Delete(It.IsAny<int>()) as NoContentResult;

            // Assert
            Assert.IsType<NoContentResult>(response);
        }
    }
}