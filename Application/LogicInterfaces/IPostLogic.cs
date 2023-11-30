using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto dto);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
    Task UpdateAsync(PostUpdateDto todo);
    Task DeleteAsync(int id);
    Task<GetPostIdDto> GetByIdAsync(int id);
}