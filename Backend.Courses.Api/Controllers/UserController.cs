using Microsoft.AspNetCore.Mvc;
using Backend.Courses.Application.Common;
using Backend.Courses.Domain.Entities;

namespace Backend.Courses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IRepository<User> _userRepository;

    public UserController(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAll()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> Create(User user)
    {
        await _userRepository.AddAsync(user);
        return CreatedAtAction(nameof(GetById), new { id = user.IdUser }, user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, User user)
    {
        await _userRepository.UpdateAsync(id, user);
        var userUpdated = await _userRepository.GetByIdAsync(id);
        return Ok(new { message = $"User {id} updated successfully", userUpdated });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        await _userRepository.DeleteAsync(id);
        return Ok(new { message = $"User {id} deleted successfully" });
    }
}
