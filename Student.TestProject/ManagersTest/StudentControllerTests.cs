using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Student.ServiceLayer.Abstract;
using Student.ServiceLayer.Models;
using StudentApi.Controllers;

namespace Student.TestProject.ManagersTest
{
    [TestFixture]
    public class StudentControllerTests
    {
        private StudentController controllerTest;
        private Mock<IStudentManager> managerTest;

        [SetUp]
        public void SetUp()
        {
            managerTest = new Mock<IStudentManager>();
            controllerTest = new StudentController(managerTest.Object);
        }

        [Test]
        public void GetStudent_idRequest_ShouldReturnModel()
        {
            // Arrange 
            var student = new StudentGetResponse {Name = "Pavel", Surname = "Ts", Gender = "М"};
            managerTest.Setup(m => m.GetStudent(It.IsAny<int>())).Returns(student);

            // Act 
            var result = controllerTest.GetStudent(1);

            // Assert 
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            var studentResult = okResult.Value as StudentGetResponse;
            Assert.AreEqual(studentResult, student);
        }

        [Test]
        public void CreateStudent_AddNewStudent_ShouldReturnOk()
        {
            // Arrange 
            var student = new StudentCreateRequest();
            managerTest.Setup(m => m.CreateStudent(It.IsAny<StudentCreateRequest>())).Returns(new ResponseResult { Message = "Ok", IsSuccess = true });

            // Act 
            var result = controllerTest.CreateStudent(student);

            // Assert 
            Assert.IsNotNull(result);
            managerTest.Verify(m => m.CreateStudent(It.IsAny<StudentCreateRequest>()), Times.Once);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(okResult.Value, "Ok");
        }

        [Test]
        public void CreateStudent_AddNewStudent_ShouldReturnError()
        {
            // Arrange 
            var student = new StudentCreateRequest();
            managerTest.Setup(m => m.CreateStudent(It.IsAny<StudentCreateRequest>())).Returns(new ResponseResult { Message = "Error", IsSuccess = false });

            // Act 
            var result = controllerTest.CreateStudent(student);

            // Assert 
            Assert.IsNotNull(result);
            managerTest.Verify(m => m.CreateStudent(It.IsAny<StudentCreateRequest>()), Times.Once);
            var badRequest = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequest.Value, "Error");
        }

        [Test]
        public void UpdateStudent_UpdateExistStudent_ShouldReturnOk()
        {
            // Arrange 
            var student = new StudentUpdateRequest();
            managerTest.Setup(m => m.UpdateStudent(It.IsAny<StudentUpdateRequest>()))
                .Returns(new ResponseResult {Message = "Ok", IsSuccess = true});

            // Act 
            var result = controllerTest.UpdateStudent(student);

            // Assert 
            Assert.IsNotNull(result);
            managerTest.Verify(m => m.UpdateStudent(It.IsAny<StudentUpdateRequest>()), Times.Once);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(okResult.Value, "Ok");
        }

        [Test]
        public void DeleteStudent_DeleteExistStudent_ShouldReturnOk()
        {
            // Arrange 
            var studentId = 1;
            managerTest.Setup(m => m.DeleteStudent(It.IsAny<int>())).Returns(new ResponseResult { Message = "Ok", IsSuccess = true });

            // Act 
            var result = controllerTest.DeleteStudent(studentId);

            // Assert 
            Assert.IsNotNull(result);
            managerTest.Verify(m => m.DeleteStudent(It.Is<int>(x => x == studentId)), Times.Once);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(okResult.Value, "Ok");
        }
    }
}
