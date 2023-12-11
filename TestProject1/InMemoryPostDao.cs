using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace TestProject1;

public class InMemoryPostDao : IPostDao
{
    private readonly List<Post> posts = new List<Post>();

    public Task<Post> CreateAsync(Post post)
    {
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        return Task.FromResult(posts.AsEnumerable());
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IEnumerable<Post> filteredPosts = posts;

        if (searchParameters.Id.HasValue)
        {
            filteredPosts = filteredPosts.Where(p => p.Id == searchParameters.Id.Value);
        }

        if (!string.IsNullOrWhiteSpace(searchParameters.Title))
        {
            filteredPosts = filteredPosts.Where(p => p.Title.Contains(searchParameters.Title));
        }

        // Additional filtering based on other parameters...

        return Task.FromResult(filteredPosts);
    }

    public Task<Post?> GetPostByIdAsync(int postId)
    {
        var post = posts.FirstOrDefault(p => p.Id == postId);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        // Assume updating means replacing the existing post with the new one
        var existingPost = posts.FirstOrDefault(p => p.Id == post.Id);

        if (existingPost != null)
        {
            posts.Remove(existingPost);
            posts.Add(post);
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int postId)
    {
        var postToRemove = posts.FirstOrDefault(p => p.Id == postId);
        if (postToRemove != null)
        {
            posts.Remove(postToRemove);
        }

        return Task.CompletedTask;
    }
}