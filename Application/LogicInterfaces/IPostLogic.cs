using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

/// Defines the contract for post-related business logic operations
public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto postToCreate);
    Task<IEnumerable<Post>> GetAllPostsAsync();
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
    Task UpdateAsync(PostUpdateDto updateDto);
    Task<Post?> GetPostByIdAsync(int postId);
    Task DeleteAsync(int id);
}