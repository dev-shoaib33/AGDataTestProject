using AGDataTestProject.Interfaces;
using AGDataTestProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMemoryCache _cache;
    private readonly IUserService _userService;

    public UserController(IMemoryCache cache, IUserService userService)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _cache.GetOrCreate("allUsers", entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(10);
            return _userService.GetAllUsers();
        });

        return Ok(users);
    }

    [HttpPost("add")]
    public IActionResult AddUser(User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = _userService.AddUser(user);

        if (result.IsSuccess)
        {
            _cache.Remove("allUsers"); // Invalidate cache
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }

    [HttpPost("update")]
    public IActionResult UpdateUser(User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = _userService.UpdateUser(user);

        if (result.IsSuccess)
        {
            _cache.Remove("allUsers"); // Invalidate cache
            return Ok(result.Message);
        }

        return NotFound(result.Message); // Return NotFound if user not found for update
    }

    [HttpDelete("{name}")]
    public IActionResult DeleteUser(string name)
    {
        var result = _userService.DeleteUser(name);

        if (result.IsSuccess)
        {
            _cache.Remove("allUsers"); // Invalidate cache
            return Ok(result.Message);
        }

        return NotFound(result.Message); // Return NotFound for delete if user not found
    }
}
