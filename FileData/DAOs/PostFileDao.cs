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
        int postId = 1;
        if (context.Posts.Any())
        {
            postId = context.Posts.Max(p => p.Id);
            postId++;
        }

        post.Id = postId;

        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetAllPosts()
    {
        return Task.FromResult(context.Posts.AsEnumerable());
    }

    public Task<Post?> GetPostById(int postId)
    {
        Post? existing = context.Posts.FirstOrDefault(p =>
            p.Id == postId
        );
        return Task.FromResult(existing);
    }
}