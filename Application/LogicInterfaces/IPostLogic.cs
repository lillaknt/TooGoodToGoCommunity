using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;


    public interface IPostLogic
    {
        
        Task<Post> CreateAsync(PostCreationDto postToCreate);
        // when authentication is in place
        // Task<Post> CreateAsync(PostCreationDto postToCreate, User creator);
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post?> GetPostById(int postId);
    }


