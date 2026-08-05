namespace StudentWebPortal.Model.Entity
{
    public class Student
    {
        public int StudentId { get; set; }
        public required string StudentName { get; set; }
        public int PhoneNumber { get; set; }
        public string?   Email { get; set; }
        public required string Grade{ get; set; }
        public required string Attendance  { get; set; }
    }
}
