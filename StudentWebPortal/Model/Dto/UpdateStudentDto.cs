using System.ComponentModel.DataAnnotations;

namespace StudentWebPortal.Model.Dto
{
    public class UpdateStudentDto
    {
        [StringLength(100)]
        public string? StudentName { get; set; }
    
        [EmailAddress, StringLength(100)]
        public string? Email { get; set; }


        public string? PhoneNumber { get; set; }

        public DateTime? EnrollmentDate { get; set; }

        [StringLength(250)]
        public string? Notes { get; set; }

        // No StudentId, IsActive, CreatedAt, UpdatedAt —
        // those stay server-controlled, never bound from client input.
    }
}