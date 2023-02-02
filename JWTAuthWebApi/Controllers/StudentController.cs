using JWTAuthWebApi.Entities;
using JWTAuthWebApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthWebApi.Controllers
{
    [CustomAuthorizeAttribute]
    [ApiVersion("2")]
    [ApiController]

    [Route("api/[controller]/{v:apiVersion}/[action]")]
    public class StudentController : Controller
    {
        [HttpPost]
        public IActionResult AddStudents(Student student)
        {
            
            using (var context = new SchoolContext())
            {
                context.Students.Add(student);
                context.SaveChanges();
            }

            return Ok(student);
        }

        [HttpGet]
        public IActionResult GetStudents([FromQuery] int studentId)
        {
           var student =  new SchoolContext().Students.Where(student => student.StudentId == studentId);

            return Ok(student);
        }

        [HttpPut]
        public IActionResult UpdateStudents(Student student)
        {
            using (var context = new SchoolContext())
            {
                context.Update<Student>(student);
                context.SaveChanges();

            }
            return Ok(student);
        }


        [HttpPut]
        public IActionResult AddStandardAndCourse()
        {

            using (var context = new SchoolContext())
            {
                using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var grade = context.Grades.Add(new Grade() { GradeName = "1st Grade" });

                        context.Students.Add(new Student()
                        {
                            FirstName = "Rama",
                            LastName = "Lakhan",
                            GradeId = 1
                        });

                        context.SaveChanges();

                        context.Courses.Add(new Course() { CourseName = "Computer Science" });
                        context.SaveChanges();

                        transaction.Commit();
                        return Ok();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return BadRequest();
                    }
                }
            }
        }   
    }
}
