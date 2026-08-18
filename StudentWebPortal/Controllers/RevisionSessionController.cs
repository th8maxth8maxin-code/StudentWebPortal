using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentWebPortal.Data;
using StudentWebPortal.Model.Dto;
using StudentWebPortal.Model.Entity;
using StudentWebPortal.Model.Entity.Enum;

namespace StudentWebPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RevisionSessionController(StudentWebPortalContext context) : ControllerBase
    {
        // GET: api/RevisionSessionApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RevisionSession>>> GetAll(
            [FromQuery] int? studentId,
            [FromQuery] SessionStatus? status,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate)
        {
            var query = context.RevisionSessions
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

        // GET: api/RevisionSessionApi/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RevisionSession>> GetById(int id)
        {
            var session = await context.RevisionSessions
                .Include(s => s.Student)
                .Include(s => s.Surah)
                .Include(s => s.Rank)
                .Include(s => s.RecordedBy)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (session == null)
                return NotFound();

            return Ok(ToDto(session));
        }

        // POST: api/RevisionSessionApi
        [HttpPost]
        public async Task<ActionResult<RevisionSession>> Create([FromBody] RevisionSessionCreateDto RevisionSessionCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var studentExists = await context.RevisionSessions.AnyAsync(s => s.Id == RevisionSessionCreateDto.StudentId);
            if (!studentExists)
                return BadRequest($"Student with Id {RevisionSessionCreateDto.StudentId} does not exist.");

            var session = new RevisionSession
            {
                SessionDate = RevisionSessionCreateDto.SessionDate,
                Surah = RevisionSessionCreateDto.Surah,
                VerseStart = RevisionSessionCreateDto.VerseStart,
                VerseEnd = RevisionSessionCreateDto.VerseEnd,
                Status = RevisionSessionCreateDto.Status,
                Rank = RevisionSessionCreateDto.Rank,
                DurationMinutes = RevisionSessionCreateDto.DurationMinutes,
                Notes = RevisionSessionCreateDto.Notes,
                RecordedBy = RevisionSessionCreateDto.RecordedBy,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.RevisionSessions.Add(session);
            await context.SaveChangesAsync();

            var created = await context.RevisionSessions
                .Include(s => s.Student)
                .Include(s => s.Surah)
                .Include(s => s.Rank)
                .Include(s => s.RecordedBy)
                .FirstAsync(s => s.Id == session.Id);

            return CreatedAtAction(nameof(GetById), new { id = session.Id }, ToDto(created));
        }

        // PUT: api/RevisionSessionApi/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] RevisionSessionUpdateDto RevisionSessionUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var session = await context.RevisionSessions.FindAsync(id);
            if (session == null)
                return NotFound();

            // Optimistic concurrency check
            if (RevisionSessionUpdateDto.RowVersion != null)
                context.Entry(session).Property("RowVersion").OriginalValue = RevisionSessionUpdateDto.RowVersion;

            session.StudentId = RevisionSessionUpdateDto.StudentId;
            session.SessionDate = RevisionSessionUpdateDto.SessionDate;
            session.Surah = RevisionSessionUpdateDto.Surah;
            session.VerseStart = RevisionSessionUpdateDto.VerseStart;
            session.VerseEnd = RevisionSessionUpdateDto.VerseEnd;
            session.Status = RevisionSessionUpdateDto.Status;
            session.Rank = RevisionSessionUpdateDto.Rank;
            session.DurationMinutes = RevisionSessionUpdateDto.DurationMinutes;
            session.Notes = RevisionSessionUpdateDto.Notes;
            session.RecordedBy = RevisionSessionUpdateDto.RecordedBy;
            session.UpdatedAt = DateTime.UtcNow;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await context.RevisionSessions.AnyAsync(s => s.Id == id))
                    return NotFound();

                return Conflict("The record was modified by another user. Please reload and try again.");
            }

            return NoContent();
        }

        // DELETE: api/RevisionSessionApi/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var session = await context.RevisionSessions.FindAsync(id);
            if (session == null)
                return NotFound();

            context.RevisionSessions.Remove(session);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private static RevisionSessionDto ToDto(RevisionSession s) => new RevisionSessionDto
        {
            Id = s.Id,
            StudentId = s.StudentId,
            Student = s.Student?.StudentName,
            SessionDate = s.SessionDate,
            Surah = s.Surah,
            VerseStart = s.VerseStart,
            VerseEnd = s.VerseEnd,
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
