using Domain.Models;
using NUnit.Framework;

namespace TestProject1;

public class UserDaoUnitTest
{
    [TestFixture]
    public class UserFileDaoTests
    {
        [Test]
        public async Task CreateAsync_ValidUser_ReturnsUserWithId()
        {
            // Arrange
            var userDao = new InMemoryUserDao();

            // Act
            var userToCreate = new User
            {
                Email = "test@example.com",
                Password = "password",
                FirstName = "John",
                PostCode = 12345
                // Add other properties as needed
            };

            var createdUser = await userDao.CreateAsync(userToCreate);

            // Assert
            Assert.IsNotNull(createdUser);
            Assert.That(createdUser.Email, Is.EqualTo(userToCreate.Email));
            Assert.That(createdUser.Password, Is.EqualTo(userToCreate.Password));
            Assert.That(createdUser.FirstName, Is.EqualTo(userToCreate.FirstName));
            Assert.That(createdUser.PostCode, Is.EqualTo(userToCreate.PostCode));
            Assert.That(createdUser.Id, Is.Not.EqualTo(0)); // Id should be assigned
        }
    }
}