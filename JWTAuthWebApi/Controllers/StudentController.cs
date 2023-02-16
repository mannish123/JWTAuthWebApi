using JWTAuthWebApi.Entities;
using JWTAuthWebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
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
        SchoolContext _schoolContext;
        public StudentController()
        {
            _schoolContext = new SchoolContext();
        }
        [HttpPost]
        public IActionResult AddStudents(Student student)
        {
            
            using (var context = _schoolContext)
            {
                context.Students.Add(student);
                context.SaveChanges();
            }

            return Ok(student);
        }

        [HttpGet]
        public IActionResult GetStudents([FromQuery] int studentId)
        {
           var student = _schoolContext.Students.Where(student => student.StudentId == studentId);

            return Ok(student);
        }
        
        [HttpGet]
        public IActionResult GetStandardGroupByStudents()
        {
        
            var student = from stud in _schoolContext.Students
                          join stand in _schoolContext.Standards on stud.StandardId equals stand.Id
                        group stand by stand.StandardName into standards
                        select new
                        {
                            Standards = standards.Key,
                            Students = standards.Count()
                        };

            return Ok(student);
        }

        [HttpPut]
        public IActionResult UpdateStudents(Student student)
        {
            using (var context = _schoolContext)
            {
                context.Update<Student>(student);
                context.SaveChanges();

            }


            return Ok(student);
        }
        
        [HttpPut]
        public IActionResult UpdatePutGradeName(Grade grade)
        {
            using (var context = _schoolContext)
            {
                context.Update<Grade>(grade);
                context.SaveChanges();

            }


            return Ok(grade);
        }
        
        [HttpPatch]
        public IActionResult UpdatePatchGradeName(Grade grade)
        {
            using (var context = _schoolContext)
            {
                context.Update<Grade>(grade);
                context.SaveChanges();

            }


            return Ok(grade);
        }


        [HttpPut]
        public IActionResult AddStandardAndCourse()
        {

            using (var context = _schoolContext)
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
