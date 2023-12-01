using System.Net;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserLogic userLogic;

    public UserController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync(UserCreationDto dto)
    {
        try
        {
            User user = await userLogic.CreateAsync(dto);
            return Created($"/users/{user.Id}", user);
        }
        catch (UnavailableEmailException ex)
        {
            return Conflict(new { Message = ex.Message });
        }
        catch (InvalidNameLengthException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (InvalidEmailException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
        }
    }
    
    [HttpGet] //client can request data
    public async Task<ActionResult<IEnumerable<User>>> GetAsync([FromQuery] string? email)
    {
        try
        {
            SearchUserParametersDto parameters = new(email);
            IEnumerable<User> users = await userLogic.GetAsync(parameters);
            return Ok(users);
        }
        catch (Exception ex)
        {
            // Handle exceptions
            return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
        }
    }
}