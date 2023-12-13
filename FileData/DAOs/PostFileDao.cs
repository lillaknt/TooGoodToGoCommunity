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
        // Generate a unique ID for the new post
        var postId = 1;
        if (context.Posts.Any())
        {
            // Add the new post to the context and save changes
            postId = context.Posts.Max(p => p.Id);
            postId++;
        }

        post.Id = postId;

        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }


    public Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        // Retrieve all posts from the context

        return Task.FromResult(context.Posts.AsEnumerable());
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        // Filter posts based on search parameters

        var posts = context.Posts.AsEnumerable();

        if (searchParameters.Id.HasValue) posts = posts.Where(p => p.Id == searchParameters.Id.Value);

        if (!string.IsNullOrWhiteSpace(searchParameters.Title))
            posts = posts.Where(p => p.Title.Contains(searchParameters.Title, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(searchParameters.Description))
            posts = posts.Where(p =>
                p.Description.Contains(searchParameters.Description, StringComparison.OrdinalIgnoreCase));

        if (searchParameters.Price.HasValue) posts = posts.Where(p => p.Price == searchParameters.Price.Value);

        return Task.FromResult(posts);
    }


    public Task<Post?> GetPostByIdAsync(int postId)
    {
        // Retrieve a post by its ID from the context

        var post = context.Posts.FirstOrDefault(p => p.Id == postId);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post updateDto)
    {
        // Retrieve the existing post by ID

        var existingPost = context.Posts.FirstOrDefault(p => p.Id == updateDto.Id);

        if (existingPost == null) throw new KeyNotFoundException($"Post with ID {updateDto.Id} not found.");
        // Remove the existing post and add the updated post

        context.Posts.Remove(existingPost);
        context.Posts.Add(updateDto);

        // Save changes to the context
        context.SaveChanges();

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var existingPost = context.Posts.FirstOrDefault(p => p.Id == id);
        if (existingPost == null) throw new KeyNotFoundException($"Post with ID {id} not found.");


        // Remove the existing post and save changes to the context

        context.Posts.Remove(existingPost);
        context.SaveChanges();
        return Task.CompletedTask;
    }
}