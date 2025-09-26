using Microsoft.AspNetCore.Mvc;
using Backend.Courses.Application.Common;
using Backend.Courses.Domain.Entities;

namespace Backend.Courses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonController : ControllerBase
{
    private readonly IRepository<Lesson> _lessonRepository;

    public LessonController(IRepository<Lesson> lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Lesson>>> GetAll()
    {
        var lessons = await _lessonRepository.GetAllAsync();
        return Ok(lessons);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Lesson>> GetById(string id)
    {
        var lesson = await _lessonRepository.GetByIdAsync(id);
        if (lesson == null) return NotFound();
        return Ok(lesson);
    }

    [HttpGet("by-course/{courseId}")]
    public async Task<ActionResult<List<Lesson>>> GetLessonsByCourse(string courseId)
    {
        var lessons = await _lessonRepository.FindAsync(l => l.IdCourse == courseId);
        return Ok(lessons);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Lesson lesson, [FromServices] IRepository<Course> courseRepository)
    {
        var course = await courseRepository.GetByIdAsync(lesson.IdCourse);
        if (course == null)
        {
            return BadRequest("El IdCurso no corresponde a un curso existente.");
        }

        await _lessonRepository.AddAsync(lesson);
        return CreatedAtAction(nameof(GetById), new { id = lesson.IdLesson }, lesson);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, Lesson lesson)
    {
        await _lessonRepository.UpdateAsync(id, lesson);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var lesson = await _lessonRepository.GetByIdAsync(id);
        if (lesson == null) return NotFound();

        await _lessonRepository.DeleteAsync(id);
        return Ok(lesson);
    }
}
