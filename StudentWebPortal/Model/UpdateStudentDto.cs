namespace StudentWebPortal.Model
{
    public class UpdateStudentDto

    {
        public required string StudentName { get; set; }
        public int PhoneNumber { get; set; }
        public string? Email { get; set; }
        public required string Grade { get; set; }
        public required string Attendance { get; set; }
    }
}