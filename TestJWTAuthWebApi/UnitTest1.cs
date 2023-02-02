using JWTAuthWebApi.Controllers;
using JWTAuthWebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace TestJWTAuthWebApi
{
    public class UnitTest1
    {
        private StudentController _studentController;
        public UnitTest1()
        {
            _studentController = new StudentController();
        }
        [Fact]
        public void TestAddStudents()
        {
            Student student = new Student() { 
            FirstName="Shakti",
            LastName="Shiva",
            GradeId=1
            };

            var okResult = _studentController.AddStudents(student);
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);

        }

        [Fact]
        public void TestGetStudents()
        {
            
            var okResult = _studentController.GetStudents(1);
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);

        }
    }

    
}
