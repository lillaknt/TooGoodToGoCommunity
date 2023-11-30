using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    
private readonly IPostDao postDao;
private readonly IUserDao userDao;

public PostLogic(IPostDao postDao, IUserDao userDao)
{
    this.postDao = postDao;
    this.userDao = userDao;
}


    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.UserId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.UserId} was not found.");
        }

        ValidatePost(dto);
        
        Post post = new Post(new User(dto.UserId, null,null, null,null), dto.Title, dto.Body, null);
      Post created = await postDao.CreateAsync(post);
        return created;
    }
    
    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        return postDao.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(PostUpdateDto dto)
    {
        Post? existing = await postDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"post with ID {dto.Id} not found!");
        }
        
        string newTitle = dto.Title ?? existing.Title;
        string newBody = dto.Body ?? existing.Body;
        double? newPrice = dto.Price ?? existing.Price;
        
        Post updated = new Post(new User(dto.UserId, null,null, null,null),newTitle, newBody, newPrice)
        {
            Id = existing.Id,
        };

        ValidatePost(updated);

        await postDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"post with ID {id} was not found!");
        }

        await postDao.DeleteAsync(id);
    }

    public async Task<GetPostIdDto> GetByIdAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"post with id {id} not found");
        }

        return new GetPostIdDto(post.Id);
    }
   
    private void ValidatePost(PostCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        if (string.IsNullOrEmpty(dto.Body)) throw new Exception("Body cannot be empty.");
    }
    
    private void ValidatePost(Post dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        if (string.IsNullOrEmpty(dto.Body)) throw new Exception("Body cannot be empty.");
    }
}