using Application.DaoInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class PostFileDao : IPostDao
{
    
    private readonly FileContext context;
    
    public PostFileDao(FileContext context)
    { 
        this.context = context;
    }
    
    
    public Task<Post> CreateAsync(Post post)
    {
        int id = 1;
        if (context.Posts.Any())
        {
            id = context.Posts.Max(t => t.Id);
            id++;
        }

        post.Id = id;
        
        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }
}