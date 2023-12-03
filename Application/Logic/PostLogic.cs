using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic
{
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
            

            // Create a new post without associating it with a user for now
            var newPost = new Post
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                // Creator will be set later when authentication is implemented
            };

            // Save the post using the DAO
            return await postDao.CreateAsync(newPost);
        }

   

        /*
         
         When authentication is in place
         public async Task<Post> CreateAsync(PostCreationDto dto, User creator)
        {
            // Validate the post creation DTO
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Post creation DTO cannot be null.");
            }

            // You can add more validation logic for the DTO properties if needed

            // Create a new post
            var newPost = new Post
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Creator = creator // Associate the creator with the post
            };

            // Save the post using the DAO
            return await postDao.CreateAsync(newPost);
        }*/

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            // Implement logic to get all posts
            return await postDao.GetAllPostsAsync() ?? Enumerable.Empty<Post>();
        }
        
        public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
        {
            return await postDao.GetAsync(searchParameters);
        }
        
        public async Task<Post?> GetPostById(int postId)
        {
            return await postDao.GetPostByIdAsync(postId);
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

            Post updated = new(titleToUse, descriptionToUse, priceToUse)
            {
                Id = existingPost.Id,
            };

            // Save the updated post
            await postDao.UpdateAsync(updated);

            // Retrieve the updated post (optional, depending on your requirements)
           // existingPost = await postDao.GetPostByIdAsync(existingPost.Id);

           // return existingPost;
        }

        public async Task<Post?> GetPostByIdAsync(int postId)
        {
            Post? post = await postDao.GetPostByIdAsync(postId);
            if (post == null)
            {
                throw new Exception($"Post with ID {postId} not found.");

            }

            return new Post(post.Title, post.Description, post.Price);
        }


        // Implement other methods as needed
    }
}