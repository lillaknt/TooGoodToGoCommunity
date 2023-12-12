using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace TestProject1;

public class InMemoryPostDao : IPostDao
{
    private readonly List<Post> _posts = new();

    public Task<Post> CreateAsync(Post post)
    {
        _posts.Add(post);
        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        return Task.FromResult(_posts.AsEnumerable());
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IEnumerable<Post> filteredPosts = _posts;

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
        var post = _posts.FirstOrDefault(p => p.Id == postId);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        // Replacing the existing post with the new one
        var existingPost = _posts.FirstOrDefault(p => p.Id == post.Id);

        if (existingPost != null)
        {
            _posts.Remove(existingPost);
            _posts.Add(post);
            return Task.CompletedTask;
        }

        // If the post with the specified ID is not found, throw KeyNotFoundException
        throw new KeyNotFoundException($"Post with ID {post.Id} not found.");

    }

    public Task DeleteAsync(int postId)
    {
        var postToRemove = _posts.FirstOrDefault(p => p.Id == postId);
        if (postToRemove != null)
        {
            _posts.Remove(postToRemove);
        }

        return Task.CompletedTask;
    }
}