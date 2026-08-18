using StudentWebPortal.Model.Entity;
using StudentWebPortal.Model.Entity.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentWebPortal.Model.Dto
{
    public class HifzSessionDto
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SessionDate { get; set; } = DateTime.Now;

        [Required]
        public Surahs? Surah { get; set; }


        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Verse start must be a positive integer")]
        [Display(Name = "Verse Start")]
        public int VerseStart { get; set; }

        [Required]
        public SessionStatus? Status { get; set; }

        [Required]
        [Display(Name = "Rank")]
        public Ranks? Rank { get; set; }

        [Required]
        [Range(1, 300, ErrorMessage = "Duration must be between 1 and 300 minutes")]
        public int DurationMinutes { get; set; }

        [MaxLength(250)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Notes")]
        public string? Notes { get; set; }


        [Required]
        public Teachers? RecordedBy { get; set; }

        [Editable(false)]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [Editable(false)]
        [Display(Name = "Updated At")]
        public DateTime UpdatedAt { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
