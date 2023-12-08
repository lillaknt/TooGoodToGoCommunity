using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;

    public PostLogic(IPostDao postDao)
    {
        this.postDao = postDao;
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        // Validate the post creation DTO
        if (dto == null) throw new ArgumentNullException(nameof(dto), "Post creation DTO cannot be null.");

        var newPost = new Post
        {
            Title = dto.Title,
            ImageData = dto.ImageData,
            Description = dto.Description,
            Price = dto.Price,
            User = new User { Id = dto.UserId } // Associate the user with the post
        };

        // Save the post using the DAO
        return await postDao.CreateAsync(newPost);
    }

    public async Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        // Implement logic to get all posts
        return await postDao.GetAllPostsAsync() ?? Enumerable.Empty<Post>();
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        return await postDao.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(PostUpdateDto updateDto)
    {
        var existingPost = await postDao.GetPostByIdAsync(updateDto.Id);

        if (existingPost == null) throw new Exception($"Post with ID {updateDto.Id} not found.");

        // Update post properties if specified in the update DTO
        var titleToUse = updateDto.Title ?? existingPost.Title;
        var descriptionToUse = updateDto.Description ?? existingPost.Description;
        var priceToUse = updateDto.Price ?? existingPost.Price ?? 0;

        var imageDataToUse = updateDto.ImageData ?? existingPost.ImageData;

        Post updated = new(titleToUse, descriptionToUse, priceToUse, imageDataToUse,
            new User { Id = existingPost.User.Id })
        {
            Id = existingPost.Id
        };

        // Save the updated post
        await postDao.UpdateAsync(updated);
    }

    public async Task<Post?> GetPostByIdAsync(int postId)
    {
        var post = await postDao.GetPostByIdAsync(postId);
        if (post == null) throw new Exception($"Post with ID {postId} not found.");

        return new Post(post.Title, post.Description, post.Price, post.ImageData, new User { Id = post.User.Id });
    }

    public async Task DeleteAsync(int id)
    {
        var existingPost = await postDao.GetPostByIdAsync(id);

        if (existingPost == null) throw new Exception($"Post with ID {id} not found.");


        await postDao.DeleteAsync(id);
    }

    public async Task<Post?> GetPostById(int postId)
    {
        return await postDao.GetPostByIdAsync(postId);
    }
}