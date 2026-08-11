using StudentWebPortal.Model.Entity.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentWebPortal.Model.Entity
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        public Student? Student { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AttendanceDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Attendance status is required.")]
        [EnumDataType(typeof(SessionStatus), ErrorMessage = "Invalid attendance status selected.")]
        [Display(Name = "Status")]
        public SessionStatus? SessionStatus { get; set; }

        [MaxLength(250)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Attendance RecordedBy is required.")]
        [EnumDataType(typeof(Teachers), ErrorMessage = "Invalid attendance RecordedBy selected.")]
        [Display(Name = "Recorded By")]
        public Teachers? RecordedBy { get; set; }

        [Editable(false)]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Editable(false)]
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }




    }
}
