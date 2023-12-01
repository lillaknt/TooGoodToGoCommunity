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

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            // Implement logic to get all posts
            return await postDao.GetAllPosts();
        }

        public async Task<Post?> GetPostById(int postId)
        {
            // Implement logic to get a post by ID
            return await postDao.GetPostById(postId);
        }

        // Implement other methods as needed
    }
}