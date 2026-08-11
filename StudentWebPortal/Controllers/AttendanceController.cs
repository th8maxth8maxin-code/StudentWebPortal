using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentWebPortal.Data;
using StudentWebPortal.Model.Dto;
using StudentWebPortal.Model.Entity;


namespace StudentWebPortal.Controllers

{

    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController(StudentWebPortalContext context) : Controller
    {
        private const string V = "attendance/{studentid : int}";
        private readonly StudentWebPortalContext _context = context;

        // GET /api/attendance  -> all records, with student name included
        [HttpGet]
        public async Task<ActionResult> GetAllAttendances()
        {
            return Ok(_context.Attendances.ToList());
        }


        // GET /api/attendance/student/5 -> all records for one student
        [HttpGet(V)]
        public async Task<ActionResult> GetByStudentId(int studentid)
        {
            var Attendance_id = await _context.Attendances.FindAsync(studentid);
            return Attendance_id is null ? NotFound() : Ok(Attendance_id);

        }
        // POST /api/attendance
        [HttpPost]
        public async Task<ActionResult> CreateAttendance([FromBody] AttendanceCreateDto CreateUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var attendance = new Attendance
            {
                RecordedBy = CreateUpdateDto.RecordedBy,
                SessionStatus = CreateUpdateDto.SessionStatus,
                Notes = CreateUpdateDto.Notes
                // IsActive defaults to true from the entity itself
                // CreatedAt/UpdatedAt handled by SaveChanges override
            };
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByStudentId), new { id = attendance.StudentId }, attendance);


        }
        // PUT /api/attendance/5
        [HttpPut(V)]
        public async Task<IActionResult> UpdateAttendance(int studentid, [FromBody] UpdateAttendanceDto updateAttendanceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var student_id = await _context.Attendances.FindAsync(studentid);
            if (student_id == null)
                return NotFound();

            student_id.SessionStatus = updateAttendanceDto.SessionStatus;
            student_id.Notes = updateAttendanceDto.Notes;
            student_id.RecordedBy = updateAttendanceDto.RecordedBy;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Attendances.Any(e => e.StudentId == studentid))
                    return NotFound();


            }
            return NoContent();


        }
        // DELETE /api/attendance/5
        [HttpDelete(V)]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var record = await _context.Attendances.FindAsync(id);
            if (record is null) return NotFound();

            _context.Attendances.Remove(record);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
