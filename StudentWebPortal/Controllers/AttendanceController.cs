using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentWebPortal.Data;
using StudentWebPortal.Model.Entity;


namespace StudentWebPortal.Controllers

{

    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : Controller
    {
        private readonly StudentWebPortalContext _context;
        public AttendanceController(StudentWebPortalContext context)
        {
            _context = context;
        }

        // GET /api/attendance  -> all records, with student name included
        [HttpGet]
        public async Task<ActionResult<List<Attendance>>> GetAll()
            => await _context.Attendances.Include(a => a.Student).ToListAsync();

        // GET /api/attendance/student/5 -> all records for one student
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<List<Attendance>>> GetByStudent(int studentId)
            => await _context.Attendances.Where(a => a.StudentId == studentId).ToListAsync();
        // POST /api/attendance
        [HttpPost]
        public async Task<ActionResult<Attendance>> Create(Attendance record)
        {
            _context.Attendances.Add(record);
            await _context.SaveChangesAsync();
            return Ok(record);
        }
        // PUT /api/attendance/5
        [HttpPost]
        public async Task<ActionResult<Attendance>> Update(int id, Attendance updatedRecord)
        {
            if (id != updatedRecord.Id)
                return BadRequest("ID mismatch");
            var existingRecord = await _context.Attendances.FindAsync(id);
            if (existingRecord == null)
                return NotFound();
            // Update fields
            existingRecord.StudentId = updatedRecord.StudentId;
            existingRecord.AttendanceDate = updatedRecord.AttendanceDate;
            existingRecord.Status = updatedRecord.Status;
            existingRecord.Notes = updatedRecord.Notes;
            existingRecord.RecordedBy = updatedRecord.RecordedBy;
            await _context.SaveChangesAsync();
            return Ok(existingRecord);
        }
        // DELETE /api/attendance/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _context.Attendances.FindAsync(id);
            if (record is null) return NotFound();

            _context.Attendances.Remove(record);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
