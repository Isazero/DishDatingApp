using DishDatingApp.Domain.Common;
using DishDatingApp.Domain.Dtos;
using DishDatingApp.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DishDatingApp.API.Controllers;

//Add authorization and login with JWT tokens
[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Register a new user",
        Description = "Registers a new user with their name, gender, age, and favorite dishes.")]
    [SwaggerResponse(StatusCodes.Status200OK, "User registered successfully", typeof(UserDto))]
    public async Task<IActionResult> Register(RegisterUserDto registerUserDto, CancellationToken cancellationToken)
    {
        var user = await userService.RegisterUser(registerUserDto, cancellationToken);
        return Ok(user);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update user details",
        Description = "Updates the user's name, gender, age, and favorite dishes.")]
    [SwaggerResponse(StatusCodes.Status200OK, "User updated successfully", typeof(UserDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> Update(Guid id, UserDto updateUserDto, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userService.UpdateUser(id, updateUserDto, cancellationToken);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get users with filters",
        Description = "Gets a list of users filtered by gender, age range, and optional favorite dishes.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Users retrieved successfully", typeof(IEnumerable<UserDto>))]
    public async Task<IActionResult> GetUsers([FromQuery] Gender gender, [FromQuery] int minAge, [FromQuery] int maxAge,
        [FromQuery] List<string> dishes, CancellationToken cancellationToken)
    {
        var users = await userService.GetUsers(gender, minAge, maxAge, dishes, cancellationToken);
        return Ok(users);
    }

    // Later will need to add separate controller for dishes 
    // Functionality: CRUD operations with Image Loading, Event that you have a match with another user 
    [HttpPost("{userId}/like/{dishId}")]
    [SwaggerOperation(Summary = "Like a dish", Description = "Sends a like to a specific dish of a user.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Dish liked successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User or dish not found")]
    public async Task<IActionResult> LikeDish(Guid userId, Guid dishId, CancellationToken cancellationToken)
    {
        try
        {
            await userService.LikeDish(userId, dishId, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
