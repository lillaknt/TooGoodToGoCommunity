using System.Collections;
using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(PostCreationDto dto);
    Task<ICollection<Post>> GetAsync(
        string? Title, 
        string? Description, 
        decimal? Price
    );
    Task<IEnumerable<Post>> GetId(GetPostIdDto id);

    Task<PostUpdateDto> GetByIdAsync(int id, int userid);
    
    Task UpdateAsync(PostUpdateDto dto);
    
    Task DeleteAsync(int id);
    
    
}