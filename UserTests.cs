using BlatchAPI.Database;
using BlatchAPI.Entities;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace BlatchTests
{
    public class UserTests
    {
        [Fact]
        public async Task CreateAddresses_Should_Generate_Guid()
        {
            //Arrange
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var dataAccess = new DataAccess(config);

            var testAddress = new Address
            {
                StreetNumber = "123",
                StreetName = "Test St",
                City = "TestCity",
                StateOrCounty = "TS",
                PostalCode = "12345",
                Country = "USA"
            };

            var testUsers = new List<User> { 
                new User
                {
                    FirstName = "Joe",
                    LastName = "Bloggs",
                    Email = "1@1.com",
                    Phone = "123-456-7890",
                    Address = testAddress,
                    Age = 21,
                    Gender = "Male",
                    Company = "TestCorp",
                    Department = "Testing",
                    HeadshotImage = "http://example.com/image.jpg",
                    Longitude = -123.456,
                    Latitude = 45.678,
                    Skills = new List<string> { "C#", ".NET" },
                    Colleagues = new List<string> { "Jane Smith", "Bob Johnson" },
                    EmploymentStart = DateTimeOffset.Now.AddYears(-1),
                    EmploymentEnd = null,
                    FullName = "Joe Bloggs"
                }
            };

            //Act
            await dataAccess.CreateUserAddresses(testUsers);

            //Assert
            Assert.NotEqual(Guid.Empty, testAddress.ID);            
        }

        [Fact]
        public async Task CreateUsers_Should_SaveToDb()
        {
            //Arrange
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var dataAccess = new DataAccess(config);

            var testAddress = new Address
            {
                StreetNumber = "123",
                StreetName = "Test St",
                City = "TestCity",
                StateOrCounty = "TS",
                PostalCode = "12345",
                Country = "USA"
            };

            var testUsers = new List<User> {
                new User
                {
                    ID = Guid.NewGuid(),
                    FirstName = "Joe",
                    LastName = "Bloggs",
                    Email = "1@1.com",
                    Phone = "123-456-7890",
                    Address = testAddress,
                    Age = 21,
                    Gender = "Male",
                    Company = "TestCorp",
                    Department = "Testing",
                    HeadshotImage = "http://example.com/image.jpg",
                    Longitude = -123.456,
                    Latitude = 45.678,
                    Skills = new List<string> { "C#", ".NET" },
                    Colleagues = new List<string> { "Jane Smith", "Bob Johnson" },
                    EmploymentStart = DateTimeOffset.Now.AddYears(-1),
                    EmploymentEnd = null,
                    FullName = "Joe Bloggs"
                }
            };

            //Act
            await dataAccess.CreateUserAddresses(testUsers);
            await dataAccess.CreateUsers(testUsers);
            var usersFromDb = await dataAccess.GetAllUsers();

            //Assert
            Assert.NotNull(usersFromDb);
            Assert.Equal(testUsers.First().FirstName, usersFromDb.First().FirstName);
            Assert.Equal(testUsers.First().LastName, usersFromDb.First().LastName);
            Assert.Equal(testUsers.First().Email, usersFromDb.First().Email);
            Assert.Equal(testUsers.First().Phone, usersFromDb.First().Phone);
            Assert.Equal(testUsers.First().Address, usersFromDb.First().Address);
            Assert.Equal(testUsers.First().Age, usersFromDb.First().Age);
            Assert.Equal(testUsers.First().Gender, usersFromDb.First().Gender);
            Assert.Equal(testUsers.First().Company, usersFromDb.First().Company);
            Assert.Equal(testUsers.First().Department, usersFromDb.First().Department);
            Assert.Equal(testUsers.First().HeadshotImage, usersFromDb.First().HeadshotImage);
            Assert.Equal(testUsers.First().Longitude, usersFromDb.First().Longitude);
            Assert.Equal(testUsers.First().Latitude, usersFromDb.First().Latitude);
            Assert.Equal(testUsers.First().Skills, usersFromDb.First().Skills);
            Assert.Equal(testUsers.First().Colleagues, usersFromDb.First().Colleagues);
            Assert.Equal(testUsers.First().EmploymentStart, usersFromDb.First().EmploymentStart);
            Assert.Equal(testUsers.First().EmploymentEnd, usersFromDb.First().EmploymentEnd);
            Assert.Equal(testUsers.First().FullName, usersFromDb.First().FullName);
        }
    }
}