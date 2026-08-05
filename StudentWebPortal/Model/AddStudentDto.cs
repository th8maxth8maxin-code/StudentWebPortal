namespace StudentWebPortal.Model
{
    public class AddStudentDto
    {   
        public required string StudentName { get; set; }
        public int PhoneNumber { get; set; }
        public string? Email { get; set; }
        public required string Grade { get; set; }
        public required string Attendances { get; set; }
    }
}
