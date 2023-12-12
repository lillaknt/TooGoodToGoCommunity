using Application.Logic;
using Domain.DTOs;
using Domain.Models;
namespace TestProject1;
using NUnit.Framework;

public class UnitTestUser
{
    [Test]
    public void UserTest()
    {
        // Arrange
        int id = 1;
        string email = "user@example.com";
        string firstName = "John";
        string password = "password";
        int postCode = 12345;

        // Act
        var user = new User(id, email, firstName, password, postCode);

        // Assert
        Assert.That(user.Id, Is.EqualTo(id));
        Assert.That(user.Email, Is.EqualTo(email));
        Assert.That(user.FirstName, Is.EqualTo(firstName));
        Assert.That(user.Password, Is.EqualTo(password));
        Assert.That(user.PostCode, Is.EqualTo(postCode));
    }
    
        [TestFixture]
    public class UserLogicTests
    {
        [Test]
        public void ValidateData_ValidUser_NoExceptionThrown()
        {
            // Arrange
            var userToCreate = new UserCreationDto(
                "test@example.com", 
                "John", 
                "password", 
                12345
                );

            // Act and Assert
            Assert.DoesNotThrow(() => UserLogic.ValidateData(userToCreate));
        }

        [Test]
        public void ValidateData_ShortFirstName_ThrowsException()
        {
            // Arrange
            var userToCreate = new UserCreationDto(
                "test@example.com",
                "J", // Invalid first name (less than 2 characters)
                "password",
                12345
            );

            // Act and Assert
            var exception = Assert.Throws<Exception>(() => UserLogic.ValidateData(userToCreate));
            Assert.That(exception?.Message, Is.EqualTo("First name must be at least 2 characters!"));
        }

        [Test]
        public void ValidateData_LongEmail_ThrowsException()
        {
            // Arrange
            var userToCreate = new UserCreationDto(
                "test1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890@example.com", // Invalid long email
                "John",
                "password",
                12345
            );

            // Act and Assert
            var exception = Assert.Throws<Exception>(() => UserLogic.ValidateData(userToCreate));
            Assert.That(exception?.Message, Is.EqualTo("Email must be less than 100 characters!"));
        }

        [Test]
        public void ValidateData_InvalidEmailFormat_ThrowsException()
        {
            // Arrange
            var userToCreate = new UserCreationDto(
                "invalid-email", // Invalid email format (no '@')
                "John",
                "password",
                12345
            );

            // Act and Assert
            var exception = Assert.Throws<Exception>(() => UserLogic.ValidateData(userToCreate));
            Assert.That(exception?.Message, Is.EqualTo("Invalid email format. Please include '@' in the email address!"));
        }
    }
}