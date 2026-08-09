using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentWebPortal.Data;
using StudentWebPortal.Model;
using StudentWebPortal.Model.Dto;
using StudentWebPortal.Model.Entity;

namespace StudentWebPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class StudentsController : ControllerBase
    {
        private readonly StudentWebPortalContext _context;
        public StudentsController(StudentWebPortalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(_context.Students.ToList());

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _context.Students.FindAsync(id);
            return student is null ? NotFound() : Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = new Student
            {
                StudentName = dto.StudentName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                EnrollmentDate = dto.EnrollmentDate,
                Notes = dto.Notes
                // IsActive defaults to true from the entity itself
                // CreatedAt/UpdatedAt handled by SaveChanges override
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = student.StudentId }, student);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            student.StudentName = updateStudentDto.StudentName;
            student.Email = updateStudentDto.Email;
            student.PhoneNumber = updateStudentDto.PhoneNumber;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Students.Any(s => s.StudentId == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            _context.SaveChanges();
            return Ok();
        }
    }
}
