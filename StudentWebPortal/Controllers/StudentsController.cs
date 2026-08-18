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

    public class StudentsController(StudentWebPortalContext context) : ControllerBase
    {
        private const string V = "{id:int}";
        private readonly StudentWebPortalContext _context = context;

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(_context.Students.ToList());

        }
        [HttpGet(V)]
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
                Stundent = dto.StudentName,
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
        [HttpPut(V)]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            student.Stundent = updateStudentDto.StudentName;
            student.Email = updateStudentDto.Email;
            student.PhoneNumber = updateStudentDto.PhoneNumber;
            student.EnrollmentDate = updateStudentDto.EnrollmentDate;
            student.Notes = updateStudentDto.Notes;

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
        [HttpDelete(V)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var record = await _context.Students.FindAsync(id);
            if (record is null) return NotFound();

            _context.Students.Remove(record);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
