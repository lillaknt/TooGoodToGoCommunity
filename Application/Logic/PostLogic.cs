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
        /*User? user = await userDao.GetByIdAsync(dto.UserId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.UserId} was not found.");
        }*/

        ValidatePost(dto);
        
        Post post = new Post(new User(dto.UserId, null,null, null,null), dto.Title, dto.Body, null);
      Post created = await postDao.CreateAsync(post);
        return created;
    }
    
    private void ValidatePost(PostCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        if (string.IsNullOrEmpty(dto.Body)) throw new Exception("Body cannot be empty.");
    }
}