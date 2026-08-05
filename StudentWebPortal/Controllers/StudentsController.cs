using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentWebPortal.Data;
using StudentWebPortal.Model;
using StudentWebPortal.Model.Entity;

namespace StudentWebPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext dbContext = dbContext;

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(dbContext.Students.ToList());

        }

        [HttpGet("{id:int}")]
        public IActionResult GetStudent(int id) {
            var student = dbContext.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPost]
        public IActionResult AddStudent(AddStudentDto addStudentDto) 
        {
            var studentEntity = new Student()
            {
                StudentName = addStudentDto.StudentName,
                PhoneNumber = addStudentDto.PhoneNumber,
                Email = addStudentDto.Email,
                Grade = addStudentDto.Grade,
                Attendance = addStudentDto.Attendances
            };

            dbContext.Students.Add(studentEntity);
            dbContext.SaveChanges();        

            return Ok(studentEntity);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, UpdateStudentDto updateStudentDto)
        {
            var student = dbContext.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            student.StudentName = updateStudentDto.StudentName;
            student.PhoneNumber = updateStudentDto.PhoneNumber;
            student.Email = updateStudentDto.Email;
            student.Grade = updateStudentDto.Grade;
            student.Attendance = updateStudentDto.Attendance;

            dbContext.SaveChanges();
            return Ok(student);
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = dbContext.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            dbContext.Students.Remove(student);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
