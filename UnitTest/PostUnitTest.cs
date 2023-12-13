using System.Text;
using Application.Logic;
using Domain.DTOs;
using Domain.Models;
using NUnit.Framework;

namespace TestProject1;

public class PostUnitTest
{
    [Test]
    public void Post_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var title = "Test Post";
        var description = "This is a test post.";
        var price = 99.99m;
        byte[] imageData = { 1, 2, 3 };


        var user = new User(1, "user@example.com", "John", "password", 12345, 1, .45);

        // Act
        var post = new Post(title, description, price, imageData, user);

        // Assert
        Assert.That(post.Title, Is.EqualTo(title));
        Assert.That(post.Description, Is.EqualTo(description));
        Assert.That(post.Price, Is.EqualTo(price));
        Assert.That(post.ImageData, Is.EqualTo(imageData));
        Assert.That(post.User, Is.EqualTo(user));
    }

    [Test]
    public void GetPostIdDto_SetId_ReturnId()
    {
        // Arrange
        var getIdDto = new GetPostIdDto();
        const int id = 42;

        // Act
        getIdDto.SetId(id);
        var result = getIdDto.ReturnId();

        // Assert
        Assert.That(result, Is.EqualTo(id));
    }

    [Test]
    public async Task DeleteAsync_ExistingPost_DeletesPost()
    {
        // Arrange
        var postDao = new InMemoryPostDao();
        var postLogic = new PostLogic(postDao);

        var postId = 1;
        var userId = 1;

        var existingPost = new Post
        {
            Id = postId,
            Title = "Original Title",
            Description = "Original Description",
            Price = 50.0m,
            ImageData = Encoding.UTF8.GetBytes("Original Image Data"),
            User = new User { Id = userId }
        };

        await postDao.CreateAsync(existingPost);

        // Act
        await postLogic.DeleteAsync(postId);

        // Assert
        var deletedPost = (await postDao.GetAsync(new SearchPostParametersDto { Id = postId })).FirstOrDefault();
        Assert.IsNull(deletedPost);
    }

    [Test]
    public void DeleteAsync_NonExistingPost_ThrowsException()
    {
        // Arrange
        var postDao = new InMemoryPostDao();
        var postLogic = new PostLogic(postDao);

        var nonExistingPostId = 999;

        // Act and Assert
        Assert.ThrowsAsync<Exception>(async () => await postLogic.DeleteAsync(nonExistingPostId));
    }

    [TestFixture]
    public class PostLogicTests
    {
        [Test]
        public async Task UpdateAsync_ValidUpdateDto_UpdatesPost()
        {
            // Arrange
            var postDao = new InMemoryPostDao();
            var postLogic = new PostLogic(postDao);

            var postId = 1;
            var userId = 1;

            var updateDto = new PostUpdateDto(
                postId,
                userId,
                "Updated Title",
                "Updated Description",
                99.99m,
                Encoding.UTF8.GetBytes("Updated Image Data")
            );

            var existingPost = new Post
            {
                Id = postId,
                Title = "Original Title",
                Description = "Original Description",
                Price = 50.0m,
                ImageData = Encoding.UTF8.GetBytes("Original Image Data"),
                User = new User { Id = userId }
            };

            await postDao.CreateAsync(existingPost);

            // Act
            await postLogic.UpdateAsync(updateDto);

            // Assert
            var updatedPost = (await postDao.GetAsync(new SearchPostParametersDto { Id = postId })).FirstOrDefault();
            Assert.That(updatedPost, Is.Not.Null);

            Assert.That(updatedPost?.Title, Is.EqualTo(updateDto.Title));
            Assert.That(updatedPost?.Description, Is.EqualTo(updateDto.Description));
            Assert.That(updatedPost?.Price, Is.EqualTo(updateDto.Price));
            Assert.That(updatedPost?.ImageData, Is.EqualTo(updateDto.ImageData));
            Assert.That(updatedPost.User.Id, Is.EqualTo(userId));
        }
    }
}