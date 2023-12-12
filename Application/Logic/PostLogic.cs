using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic
{ 
    /// Implements the business logic operations related to posts.

    public class PostLogic : IPostLogic
    {
        private readonly IPostDao postDao;

        public PostLogic(IPostDao postDao)
        {
            this.postDao = postDao;
        }

        public async Task<Post> CreateAsync(PostCreationDto dto)
        {
            // Validate the post creation DTO
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Post creation DTO cannot be null.");
            }
            // Create a new post using the provided DTO

            var newPost = new Post
            {
                Title = dto.Title,
                ImageData = dto.ImageData,
                Description = dto.Description,
                Price = dto.Price,
                User = new User { Id = dto.UserId } // Associate the user with the post
            };

            // Save the post using the DAO
            return await postDao.CreateAsync(newPost);
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            // Implement logic to get all posts
            return await postDao.GetAllPostsAsync() ?? Enumerable.Empty<Post>();
        }
        // Implement logic to get all posts

        public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
        {
            return await postDao.GetAsync(searchParameters);
        }

        public async Task UpdateAsync(PostUpdateDto updateDto)
        {


            Post? existingPost = await postDao.GetPostByIdAsync(updateDto.Id);

            if (existingPost == null)
            {
                throw new Exception($"Post with ID {updateDto.Id} not found.");
            }

            // Update post properties if specified in the update DTO
            string titleToUse = updateDto.Title ?? existingPost.Title;
            string descriptionToUse = updateDto.Description ?? existingPost.Description;
            decimal priceToUse = updateDto.Price ?? existingPost.Price ?? 0;

            byte[]? imageDataToUse = updateDto.ImageData ?? existingPost.ImageData;

            Post updated = new(titleToUse, descriptionToUse, priceToUse, imageDataToUse,
                new User { Id = existingPost.User.Id })
            {
                Id = existingPost.Id,
            };

            // Save the updated post
            await postDao.UpdateAsync(updated);
        }
        // Retrieve the post by ID from the DAO
        public async Task<Post?> GetPostByIdAsync(int postId)
        {
            Post? post = await postDao.GetPostByIdAsync(postId);
            if (post == null)
            {
                throw new Exception($"Post with ID {postId} not found.");

            }
            // Return a new instance of the post to avoid external modification

            return new Post(post.Title, post.Description, post.Price, post.ImageData, new User { Id = post.User.Id });
        }

        public async Task DeleteAsync(int id)
        {
            Post? existingPost = await postDao.GetPostByIdAsync(id);

            if (existingPost == null)
            {
                throw new Exception($"Post with ID {id} not found.");
            }

// Delete the post using the DAO
            await postDao.DeleteAsync(id);
        }

    }
}