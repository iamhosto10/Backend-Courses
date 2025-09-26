using Microsoft.AspNetCore.Mvc;
using Backend.Courses.Application.Common;
using Backend.Courses.Domain.Entities;

namespace Backend.Courses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly IRepository<Course> _courseRepository;

    public CourseController(IRepository<Course> courseRepository)
    {
        _courseRepository = courseRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Course>>> GetAll()
    {
        var courses = await _courseRepository.GetAllAsync();
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetById(string id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null) return NotFound();
        return Ok(course);
    }
    [HttpGet("by-professor/{professorId}")]
    public async Task<ActionResult<List<Course>>> GetCoursesByProfessor(string professorId)
    {
        var courses = await _courseRepository.FindAsync(c => c.IdProfessor == professorId);
        return Ok(courses);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Course course, [FromServices] IRepository<User> userRepository)
    {
        var professor = await userRepository.GetByIdAsync(course.IdProfessor);
        if (professor == null || professor.Role.ToLower() != "profesor")
        {
            return BadRequest("El IdProfesor no corresponde a un usuario con rol 'profesor'.");
        }

        await _courseRepository.AddAsync(course);
        return CreatedAtAction(nameof(GetById), new { id = course.IdCourse }, course);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, Course course)
    {
        await _courseRepository.UpdateAsync(id, course);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null) return NotFound();

        await _courseRepository.DeleteAsync(id);
        return Ok(course);
    }
}
