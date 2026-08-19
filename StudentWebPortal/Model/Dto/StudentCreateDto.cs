using System.ComponentModel.DataAnnotations;

namespace StudentWebPortal.Model.Dto
{
    public class StudentCreateDto
    {
        [Required, StringLength(100)]
        public required string StudentName { get; set; }

        [EmailAddress, StringLength(100)]
        public string? Email { get; set; }

        
        public string? PhoneNumber { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }

        [StringLength(250)]
        public string? Notes { get; set; }
        // No StudentId, IsActive, CreatedAt, UpdatedAt here —
        // those are server-controlled.
    }
}