using BlatchAPI.Database;
using BlatchAPI.Entities;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace BlatchTests
{
    public class AddressTests
    {
        [Fact]
        public async Task CreateAddresses_Should_Generate_Guid()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var dataAccess = new DataAccess(config);

            // Create test data
            var testAddress = new Address
            {
                StreetNumber = "123",
                StreetName = "Test St",
                City = "TestCity",
                StateOrCounty = "TS",
                PostalCode = "12345",
                Country = "USA"
            };

            await dataAccess.CreateUserAddresses(new List<Address> { testAddress });

            // Check the ID was set
            Assert.NotEqual(Guid.Empty, testAddress.ID);
        }
    }
}