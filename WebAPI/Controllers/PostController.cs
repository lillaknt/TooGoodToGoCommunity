using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

 [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostLogic postLogic;

        public PostController(IPostLogic postLogic)
        {
            this.postLogic = postLogic;
        }
        
        [HttpPost]
        public async Task<ActionResult<Post>> CreateAsync(PostCreationDto dto)
        {
            try
            {
                Post createdPost = await postLogic.CreateAsync(dto);
                return Created($"/posts/{createdPost.Id}", createdPost);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        // when authentication is in place

        /*[HttpPost]
        public async Task<ActionResult<Post>> CreateAsync(PostCreationDto dto)
        {
            try
            {
                // Get the currently logged-in user (you may need to implement authentication)
                var creator = /* Implement logic to get the current user #1#;

                if (creator == null)
                {
                    // If the user is not authenticated, return a 401 Unauthorized status
                    return Unauthorized();
                }

                Post post = await postLogic.CreateAsync(dto, creator);
                return Created($"/posts/{post.Id}", post);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }*/

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {
            try
            {
                IEnumerable<Post> posts = await postLogic.GetAllPosts();
                return Ok(posts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<Post>> GetPostById(int postId)
        {
            try
            {
                Post? post = await postLogic.GetPostById(postId);

                if (post == null)
                {
                    return NotFound(); // Return 404 Not Found if the post is not found
                }

                return Ok(post);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        // Implement other methods as needed
    }