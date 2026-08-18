using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentWebPortal.Data;       // <-- adjust to your ApplicationDbContext's namespace
using StudentWebPortal.Model.Dto;
using StudentWebPortal.Model.Entity;
using StudentWebPortal.Model.Entity.Enum;

namespace StudentWebPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HifzSessionApiController(StudentWebPortalContext context) : ControllerBase
    {

        // GET: api/HifzSessionApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HifzSession>>> GetAll(
            [FromQuery] int? studentId,
            [FromQuery] SessionStatus? status,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate)
        {
            var query = context.HifzSessions
                .Include(s => s.Student)
                .Include(s => s.Surah)
                .Include(s => s.Rank)
                .Include(s => s.RecordedBy)
                .AsQueryable();

            if (studentId.HasValue)
                query = query.Where(s => s.StudentId == studentId.Value);

            if (status.HasValue)
                query = query.Where(s => s.Status == status.Value);

            if (fromDate.HasValue)
                query = query.Where(s => s.SessionDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(s => s.SessionDate <= toDate.Value);

            var sessions = await query
                .OrderByDescending(s => s.SessionDate)
                .Select(s => ToDto(s))
                .ToListAsync();

            return Ok(sessions);
        }

        // GET: api/HifzSessionApi/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<HifzSession>> GetById(int id)
        {
            var session = await context.HifzSessions
                .Include(s => s.Student)
                .Include(s => s.Surah)
                .Include(s => s.Rank)
                .Include(s => s.RecordedBy)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (session == null)
                return NotFound();

            return Ok(ToDto(session));
        }

        // POST: api/HifzSessionApi
        [HttpPost]
        public async Task<ActionResult<HifzSession>> Create([FromBody] HifzSessionCreateDto HifzSessionCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var studentExists = await context.HifzSessions.AnyAsync(s => s.Id == HifzSessionCreateDto.StudentId);
            if (!studentExists)
                return BadRequest($"Student with Id {HifzSessionCreateDto.StudentId} does not exist.");

            var session = new HifzSession
            { 
                SessionDate = HifzSessionCreateDto.SessionDate,
                Surah = HifzSessionCreateDto.Surah,
                VerseStart = HifzSessionCreateDto.VerseStart,
                Status = HifzSessionCreateDto.Status,
                Rank = HifzSessionCreateDto.Rank,
                DurationMinutes = HifzSessionCreateDto.DurationMinutes,
                Notes = HifzSessionCreateDto.Notes,
                RecordedBy = HifzSessionCreateDto.RecordedBy,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.HifzSessions.Add(session);
            await context.SaveChangesAsync();

            var created = await context.HifzSessions
                .Include(s => s.Student)
                .Include(s => s.Surah)
                .Include(s => s.Rank)
                .Include(s => s.RecordedBy)
                .FirstAsync(s => s.Id == session.Id);

            return CreatedAtAction(nameof(GetById), new { id = session.Id }, ToDto(created));
        }

        // PUT: api/HifzSessionApi/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] HifzSessionUpdateDto HifzSessionUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var session = await context.HifzSessions.FindAsync(id);
            if (session == null)
                return NotFound();

            // Optimistic concurrency check
            if (HifzSessionUpdateDto.RowVersion != null)
                context.Entry(session).Property("RowVersion").OriginalValue = HifzSessionUpdateDto.RowVersion;

            session.SessionDate = HifzSessionUpdateDto.SessionDate;
            session.Surah = HifzSessionUpdateDto.Surah;
            session.VerseStart = HifzSessionUpdateDto.VerseStart;
            session.Status = HifzSessionUpdateDto.Status;
            session.Rank = HifzSessionUpdateDto.Rank;
            session.DurationMinutes = HifzSessionUpdateDto.DurationMinutes;
            session.Notes = HifzSessionUpdateDto.Notes;
            session.RecordedBy = HifzSessionUpdateDto.RecordedBy;
            session.UpdatedAt = DateTime.UtcNow;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await context.HifzSessions.AnyAsync(s => s.Id == id))
                    return NotFound();

                return Conflict("The record was modified by another user. Please reload and try again.");
            }

            return NoContent();
        }

        // DELETE: api/HifzSessionApi/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var session = await context.HifzSessions.FindAsync(id);
            if (session == null)
                return NotFound();

            context.HifzSessions.Remove(session);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private static HifzSessionDto ToDto(HifzSession s) => new HifzSessionDto
        {
            Id = s.Id,
            StudentId = s.StudentId,
            Student = s.Student?.Stundent,
            SessionDate = s.SessionDate,
            Surah = s.Surah,
            VerseStart = s.VerseStart,
            Status = s.Status,
            Rank = s.Rank,
            DurationMinutes = s.DurationMinutes,
            Notes = s.Notes,
            RecordedBy = s.RecordedBy,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt,
            RowVersion = s.RowVersion
        };
    }

}
