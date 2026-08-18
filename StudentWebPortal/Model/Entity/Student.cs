using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentWebPortal.Model.Entity

{
    public class Student
    {
        private const string V = "{0:yyyy-MM-dd}";

        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public required string Stundent { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }


        [RegularExpression(@"^\+?[0-9\s\-()]{7,15}$",
            ErrorMessage = "Enter a valid phone number.")]
        [StringLength(15)]
        public string? PhoneNumber { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        [Display(Name = "Enrollment Date")]
        [DisplayFormat(DataFormatString = V, ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }



        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;


        [Display(Name = "Notes")]
        [MaxLength(250)]
        [DataType(DataType.MultilineText)]
        public string? Notes { get; set; }

        [Editable(false)]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Editable(false)]
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }

        public static implicit operator Student?(string? v)
        {
            throw new NotImplementedException();
        }
    }
}
