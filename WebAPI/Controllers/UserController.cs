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
            var user = await userLogic.CreateAsync(dto);
            return Created($"/user/{user.Id}", user);
        }
        catch (UnavailableEmailException ex)
        {
            return Conflict(new { ex.Message });
        }
        catch (InvalidNameLengthException ex)
        {
            return BadRequest(new { ex.Message });
        }
        catch (InvalidEmailException ex)
        {
            return BadRequest(new { ex.Message });
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            return StatusCode((int)HttpStatusCode.InternalServerError, new { ex.Message });
        }
    }

    [HttpGet] //client can request data
    public async Task<ActionResult<IEnumerable<User>>> GetAsync([FromQuery] string? email)
    {
        try
        {
            SearchUserParametersDto parameters = new(email);
            var users = await userLogic.GetAsync(parameters);
            return Ok(users);
        }
        catch (Exception ex)
        {
            // Handle exceptions
            return StatusCode((int)HttpStatusCode.InternalServerError, new { ex.Message });
        }
    }


    [HttpPatch]
    public async Task<ActionResult> UpdateUserAsync(UserUpdateDto dto)
    {
        try
        {
            await userLogic.UpdateUserAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}