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

        [HttpGet("GetAllPosts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPostsAsync([FromQuery]int? id)
        {
            GetPostIdDto dto = new GetPostIdDto();
            dto.SetId(id);
            
            try
            {
                IEnumerable<Post> posts = await postLogic.GetAllPostsAsync();
                return Ok(posts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] SearchPostParametersDto searchParameters)
        {
            try
            {
                IEnumerable<Post> posts = await postLogic.GetAsync(searchParameters);
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
                var post = await postLogic.GetPostByIdAsync(postId);

                if (post == null)
                {
                    return NotFound(); // Return 404 Not Found if the post with the specified ID is not found
                }

                return Ok(post);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPatch]
        public async Task<ActionResult> UpdateAsync([FromBody] PostUpdateDto postUpdateDto)
        {
            try
            {
               await postLogic.UpdateAsync(postUpdateDto);
                // Return the updated post
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }  
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                await postLogic.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        

        // Implement other methods as needed
    }