using Application.DaoInterfaces;
using Domain.DTOs;
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
    
    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParams)
    {
        IEnumerable<Post> result = context.Posts.AsEnumerable();
        

        if (!string.IsNullOrEmpty(searchParams.TitleContains))
        {
            result = result.Where(t =>
                t.Title.Contains(searchParams.TitleContains, StringComparison.OrdinalIgnoreCase));
        }
        
        if (!string.IsNullOrEmpty(searchParams.BodyContains))
        {
            result = result.Where(t =>
                t.Body.Contains(searchParams.BodyContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }
    
 
    
    public Task UpdateAsync(Post toUpdate)
    {
        Post? existing = context.Posts.FirstOrDefault(todo => todo.Id == toUpdate.Id);
        if (existing == null)
        {
            throw new Exception($"Todo with id {toUpdate.Id} does not exist!");
        }

        context.Posts.Remove(existing);
        context.Posts.Add(toUpdate);
    
        context.SaveChanges();
    
        return Task.CompletedTask;
    }

    public Task<Post> GetByIdAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(existing);
    }

    public Task DeleteAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(post => post.Id == id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} does not exist!");
        }

        context.Posts.Remove(existing); 
        context.SaveChanges();
    
        return Task.CompletedTask;
    }
    
}