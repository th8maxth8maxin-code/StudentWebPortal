using System.ComponentModel.DataAnnotations;



namespace StudentWebPortal.Model.Entity

{
    public class Student
    {

        [Key]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string StudentName { get; set; }


        [Phone]
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(15)]
        [Required]
        public required string Grade { get; set; }

        [Range(0, 100)]
        public decimal Attendance { get; set; }
    }
}
