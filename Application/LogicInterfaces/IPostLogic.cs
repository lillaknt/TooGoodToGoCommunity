using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;


    public interface IPostLogic
    {
        
        Task<Post> CreateAsync(PostCreationDto postToCreate);
        // when authentication is in place
        // Task<Post> CreateAsync(PostCreationDto postToCreate, User creator);
        
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
        Task UpdateAsync(PostUpdateDto updateDto);
        
        Task<Post?> GetPostByIdAsync(int postId);
        
        Task DeleteAsync(int id);


     
    }

