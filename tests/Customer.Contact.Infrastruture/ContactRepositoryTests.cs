using Customer.Contact.Core.Entities;
using Customer.Contact.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Customer.Contact.Infrastruture
{
    public class ContactRepositoryTests
    {
        private DbContextOptions<AppDbContext> dbContextOptions;

        public ContactRepositoryTests()
        {
            var dbName = $"CustomerDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            SeedDatainDatabase();
        }

        [Fact]
        public async Task GetContactDetailsById_Returns_CustomerContactDetails()
        {
            // Arrange
            const int id = 1;
            var createContactRepository = CreateContactRepository();

            // Act
            var contactDetails = await createContactRepository.GetContactDetailsById(id);

            // Assert
            Assert.NotNull(contactDetails);
            Assert.Equal(contactDetails.Id, id);
        }

        [Fact]
        public async Task CreateContact_Returns_CreatedContactEntity()
        {
            // Arrange           
            var createContactRepository = CreateContactRepository();

            // Act
            var contactDetails = await createContactRepository.CreateContact(getContactDetails());

            // Assert            
            Assert.NotNull(contactDetails);
        }

        [Fact]
        public async Task DeleteContactDetails_Returns_TrueIfContactDetailsIsRemoved()
        {
            // Arrange           
            var createContactRepository = CreateContactRepository();

            // Act
            await createContactRepository.DeleteContactDetails(1);           
        }

        [Fact]
        public async Task GetContactDetails_Returns_AllCustomerContactDetails()
        {
            // Arrange           
            var createContactRepository = CreateContactRepository();

            // Act
            var contactDetails = await createContactRepository.GetAllContactDetails();

            // Assert            
            Assert.NotNull(contactDetails);
        }

        private ContactRepository CreateContactRepository()
        {
            AppDbContext context = new AppDbContext(dbContextOptions);
            return new ContactRepository(context);
        }

        private void SeedDatainDatabase()
        {
            AppDbContext context = new AppDbContext(dbContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Contact.Add(getContactDetails());
            context.SaveChanges();
        }

        private static Core.Entities.Contact getContactDetails()
        {
            return new Core.Entities.Contact
            {
                PhoneNumber = "+46765432897",
                Email = "Test@gmail.com",
                SocialSecurityNumber = 1234567890,
                CreatedOn = DateTime.Now,
                CreatedBy = "TestUser",
            };
        }
    }
}