using BlatchAPI.Database;
using BlatchAPI.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
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
        public async Task CreateUsersExceptForAddresses_Should_SaveToDb()
        {
            //Arrange
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var dataAccess = new DataAccess(config);

            await dataAccess.DeleteAllUsers();
            await dataAccess.DeleteAllAddresses();

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
                    Skills =  new List<string> { "C#", ".NET" },
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

            await dataAccess.DeleteAllUsers();
            await dataAccess.DeleteAllAddresses();
        }

        [Fact]
        public async Task CreateAddresses_Should_SaveToDb()
        {
            //Arrange
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var dataAccess = new DataAccess(config);

            await dataAccess.DeleteAllUsers();
            await dataAccess.DeleteAllAddresses();

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
            var addressesFromDb = await dataAccess.GetAddresses();

            //Assert
            Assert.NotNull(addressesFromDb);
            Assert.Equal(testAddress.StreetNumber, addressesFromDb.First().StreetNumber);
            Assert.Equal(testAddress.StreetName, addressesFromDb.First().StreetName);
            Assert.Equal(testAddress.City, addressesFromDb.First().City);
            Assert.Equal(testAddress.StateOrCounty, addressesFromDb.First().StateOrCounty);
            Assert.Equal(testAddress.PostalCode, addressesFromDb.First().PostalCode);
            Assert.Equal(testAddress.Country, addressesFromDb.First().Country);

            await dataAccess.DeleteAllUsers();
            await dataAccess.DeleteAllAddresses();
        }

        [Fact]
        public async Task GetAllUsers_Returns_As_Expected()

        {
            //Arrange
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var dataAccess = new DataAccess(config);

            await dataAccess.DeleteAllUsers();
            await dataAccess.DeleteAllAddresses();

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
                },
                new User
                {
                    ID = Guid.NewGuid(),
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "2@2.com",
                    Phone = "987-654-3210",
                    Address = testAddress,
                    Age = 31,
                    Gender = "Female",
                    Company = "UK GOV",
                    Department = "Development",
                    HeadshotImage = "http://example.com/image2.jpg",
                    Longitude = -98.765,
                    Latitude = 32.109,
                    Skills = new List<string> { "Java", "Spring" },
                    Colleagues = new List<string> { "Joe Bloggs", "Bob Johnson" },
                    EmploymentStart = DateTimeOffset.Now.AddYears(-2),
                    EmploymentEnd = null,
                    FullName = "Jane Smith"
                }
            };

            //Act
            await dataAccess.CreateUserAddresses(testUsers);
            await dataAccess.CreateUsers(testUsers);
            var result = await dataAccess.GetAllUsers();

            //Assert
            var joe = result.Single(u => u.FirstName == "Joe");
            Assert.Equal("Bloggs", joe.LastName);
            Assert.Equal(-123.456, joe.Longitude);
            Assert.Equal(2, joe.Skills.Count);
            Assert.Contains("C#", joe.Skills);
            Assert.Contains(".NET", joe.Skills);
            Assert.Equal(2, joe.Colleagues.Count);
            Assert.Contains("Jane Smith", joe.Colleagues);
            Assert.Contains("Bob Johnson", joe.Colleagues);

            var jane = result.Single(u => u.FirstName == "Jane");
            Assert.Equal("Smith", jane.LastName);
            Assert.Equal(-98.765, jane.Longitude);
            Assert.Equal(2, jane.Skills.Count);
            Assert.Contains("Java", jane.Skills);
            Assert.Contains("Spring", jane.Skills);
            Assert.Equal(2, jane.Colleagues.Count);
            Assert.Contains("Joe Bloggs", jane.Colleagues);
            Assert.Contains("Bob Johnson", jane.Colleagues);

            await dataAccess.DeleteAllUsers();
            await dataAccess.DeleteAllAddresses();
        }
    }
}