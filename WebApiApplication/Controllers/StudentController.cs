using Database.AppDbContextModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Models;

namespace WebApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _db;

        public StudentController()
        {
            _db = new AppDbContext();
        }

        [HttpGet("students")]
        public IActionResult GetStudents()
        {
            List<Student> studentList = _db.Students.ToList();
            return Ok(studentList);
        }

        [HttpGet("students/{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = _db.Students.FirstOrDefault(x => x.StudentId == id);

            if(student is null)
            {
                return NotFound("Student doesn't exist");
            }

            return Ok(student);
        }

        [HttpPost("create")]
        public IActionResult CreateStudent([FromBody] StudentCreateRequestModel requestDto)
        {

            Student student = new Student
            {
                StudentName = requestDto.StudentName,
                FatherName = requestDto.FatherName,
                StudentNo = requestDto.StudentNo,
                BirthDate = requestDto.BirthDate,
                PhoneNumber = requestDto.PhoneNumber,
                Address = requestDto.Address,
            };

            _db.Students.Add(student);
            int result = _db.SaveChanges();

            StudentCreateResponseModel response = new StudentCreateResponseModel
            {
                IsSuccess = result > 0 ? true : false,
                Message = result > 0 ? "Create Success" : "Create fail",
                UserId = student.StudentId
            };

            return Ok(response);

        }


        [HttpPut("upsert")]

        public IActionResult UpsertStudent()
        {
            return Ok();
        }


        [HttpPatch("edit/{id}")]
        public IActionResult PatchStudent(int id, [FromBody] StudentPatchRequestModel requestDto)
        {
            var student = _db.Students.FirstOrDefault(x => x.StudentId == id);

            if(student is null)
            {
                return NotFound(new StudentPatchResponseModel
                {
                    Message = "Student doesn't exist"
                });
            }

            if (!string.IsNullOrEmpty(requestDto.StudentName))
            {
                student.StudentName = requestDto.StudentName;
            }

            if (!string.IsNullOrEmpty(requestDto.PhoneNumber))
            {
                student.PhoneNumber = requestDto.PhoneNumber;
            }

            if (!string.IsNullOrEmpty(requestDto.FatherName))
            {
                student.FatherName = requestDto.FatherName;
            }

            if (!string.IsNullOrEmpty(requestDto.StudentNo))
            {
                student.StudentNo = requestDto.StudentNo;
            }

            if (!string.IsNullOrEmpty(requestDto.Address))
            {
                student.Address = requestDto.Address;
            }

            if (requestDto.BirthDate.HasValue)
            {
                student.BirthDate = requestDto.BirthDate.Value;
            }

            int result = _db.SaveChanges();

            StudentPatchResponseModel response = new StudentPatchResponseModel
            {
                IsSuccess = result > 0 ? true : false,
                Message = result > 0 ? "Update Success" : "Update fail",
                Data = student
            };

            return Ok(response);

        }

        [HttpDelete("delete")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _db.Students.FirstOrDefault(x => x.StudentId == id);

            if(student is null)
            {
                return NotFound(new StudentDeleteResponseModel
                {
                    Message = "Student doesn't exist"
                });
            }

            _db.Students.Remove(student);
            int result = _db.SaveChanges();

            StudentDeleteResponseModel response = new StudentDeleteResponseModel
            {
                IsSuccess = result > 0 ? true : false,
                Message = result > 0 ? "Delete Success" : "Delete fail"
            };

            return Ok(response);
        }

    }
}
