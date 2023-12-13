using Domain.Models;
using NUnit.Framework;

namespace TestProject1;

public class PostDaoUnitTest
{
    [TestFixture]
    public class PostFileDaoTests
    {
        [Test]
        public async Task UpdateAsync_ExistingPost_SuccessfullyUpdatesPost()
        {
            // Arrange
            var postDao = new InMemoryPostDao();

            // Seed the in-memory data with a post
            var initialPost = new Post
            {
                Id = 1,
                Title = "Initial Post",
                Description = "Description",
                Price = 10.99m
                // Add other properties as needed
            };

            await postDao.CreateAsync(initialPost);

            // Act
            var updatedPost = new Post
            {
                Id = 1,
                Title = "Updated Post",
                Description = "Updated Description",
                Price = 15.99m
                // Add other properties as needed
            };

            await postDao.UpdateAsync(updatedPost);

            // Assert
            var retrievedPost = await postDao.GetPostByIdAsync(1);
            Assert.IsNotNull(retrievedPost);
            Assert.That(retrievedPost?.Title, Is.EqualTo(updatedPost.Title));
            Assert.That(retrievedPost?.Description, Is.EqualTo(updatedPost.Description));
            Assert.That(retrievedPost?.Price, Is.EqualTo(updatedPost.Price));
            // Add other assertions as needed
        }

        [Test]
        public Task UpdateAsync_NonExistingPost_ThrowsKeyNotFoundException()
        {
            // Arrange
            var postDao = new InMemoryPostDao();

            // Act and Assert
            var nonExistingPost = new Post
            {
                Id = 1,
                Title = "Non-Existing Post",
                Description = "Description",
                Price = 10.99m
                // Add other properties as needed
            };

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await postDao.UpdateAsync(nonExistingPost));
            return Task.CompletedTask;
        }
    }
}