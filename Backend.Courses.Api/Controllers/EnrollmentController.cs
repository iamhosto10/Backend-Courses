using Microsoft.AspNetCore.Mvc;
using Backend.Courses.Application.Common;
using Backend.Courses.Domain.Entities;

namespace Backend.Courses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentController : ControllerBase
{
    private readonly IRepository<Enrollment> _enrollmentRepository;

    public EnrollmentController(IRepository<Enrollment> enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Enrollment>>> GetAll()
    {
        var enrollments = await _enrollmentRepository.GetAllAsync();
        return Ok(enrollments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Enrollment>> GetById(string id)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(id);
        if (enrollment == null) return NotFound();
        return Ok(enrollment);
    }

    [HttpGet("by-student/{studentId}")]
    public async Task<ActionResult<List<Enrollment>>> GetEnrollmentsByStudent(string studentId)
    {
        var enrollments = await _enrollmentRepository.FindAsync(e => e.IdUser == studentId);
        return Ok(enrollments);
    }


    [HttpPost]
    public async Task<ActionResult> Create(Enrollment enrollment,
    [FromServices] IRepository<User> userRepository,
    [FromServices] IRepository<Course> courseRepository)
    {
        var student = await userRepository.GetByIdAsync(enrollment.IdUser);
        if (student == null || student.Role.ToLower() != "estudiante")
        {
            return BadRequest("El IdUsuario no corresponde a un usuario con rol 'estudiante'.");
        }

        var course = await courseRepository.GetByIdAsync(enrollment.IdCourse);
        if (course == null)
        {
            return BadRequest("El IdCurso no corresponde a un curso existente.");
        }

        await _enrollmentRepository.AddAsync(enrollment);
        return CreatedAtAction(nameof(GetById), new { id = enrollment.IdEnrollment }, enrollment);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, Enrollment enrollment)
    {
        await _enrollmentRepository.UpdateAsync(id, enrollment);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(id);
        if (enrollment == null) return NotFound();

        await _enrollmentRepository.DeleteAsync(id);
        return Ok(enrollment);
    }
}
