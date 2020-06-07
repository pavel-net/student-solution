using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Student.ServiceLayer.Abstract;
using Student.ServiceLayer.Models;
using StudentApi.Controllers;

namespace Student.TestProject.ControllersTest
{
    // Модель AAA(размещение, действие, утверждение)
    // Подраздел Размещение метода модульного тестирования инициализирует объекты и устанавливает значение данных, которые переданы методу для теста.
    // Подраздел Действие вызывает метод для теста с размещенными параметрами.
    // Подраздел Утверждение проверяет, чтобы метод для теста действовал, как ожидается.

    [TestFixture]
    public class GroupControllerTests
    {
        private GroupController controllerTest;
        private Mock<IGroupManager> managerGroup;

        [SetUp]
        public void Setup()
        {
            managerGroup = new Mock<IGroupManager>();
            controllerTest = new GroupController(managerGroup.Object);
        }

        [Test]
        public async Task GetAllGroups_EmptyRequest_ShouldReturnModel()
        {
            // Arrange 
            const int groupIdFirst = 1;
            const int groupIdSecond = 2;
            // "It" позволяет указать условие соответствия для аргумента в вызове метода, а не конкретное значение аргумента.
            managerGroup.Setup(m => m.GetAllGroupsAsync(It.IsAny<GroupRequest>())).Returns(
                Task.FromResult(new List<GroupGetResponse>()
                {
                    new GroupGetResponse {Id = groupIdFirst},
                    new GroupGetResponse {Id = groupIdSecond}
                }));

            // Act 
            var result = await controllerTest.GetGroups(new GroupRequest());

            // Assert 
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            var groups = okResult.Value as List<GroupGetResponse>;
            Assert.AreEqual(groups.Count, 2);
            Assert.IsTrue(groups.Any(x => x.Id == groupIdFirst));
            Assert.IsTrue(groups.Any(x => x.Id == groupIdSecond));
        }

        [Test]
        public void GetGroup_idRequest_ShouldReturnModel()
        {
            // Arrange 
            var group = new GroupGetResponse {GroupName = "FirstGroup", StudentsCount = 5};
            managerGroup.Setup(m => m.GetGroup(It.IsAny<int>())).Returns(group);

            // Act 
            var result = controllerTest.GetGroup(1);

            // Assert 
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            var groupResult = okResult.Value as GroupGetResponse;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(groupResult, group);
        }

        [Test]
        public void CreateGroup_AddNewGroup_ShouldReturnOk()
        {
            // Arrange 
            const string groupName = "FirstGroup";
            managerGroup.Setup(m => m.CreateGroup(It.IsAny<string>()))
                .Returns(new ResponseResult {Message = "Ok", IsSuccess = true});

            // Act 
            var result = controllerTest.CreateGroup(groupName);

            // Assert 
            Assert.IsNotNull(result);
            // проверка на то, что метод CreateGroup с параметром groupName вызывался один раз внутри контроллера (в ходе теста)
            managerGroup.Verify(m => m.CreateGroup(It.Is<string>(x => x == groupName)), Times.Once);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(okResult.Value, "Ok");
        }

        [Test]
        public void CreateGroup_AddNewGroup_ShouldReturnError()
        {
            // Arrange 
            var groupName = "FirstGroup";
            managerGroup.Setup(m => m.CreateGroup(It.IsAny<string>())).Returns(new ResponseResult { Message = "Error", IsSuccess = false });

            // Act 
            var result = controllerTest.CreateGroup(groupName);

            // Assert 
            Assert.IsNotNull(result);
            managerGroup.Verify(m => m.CreateGroup(It.Is<string>(x => x == groupName)), Times.Once);
            var badRequest = result as BadRequestObjectResult;
            Assert.AreEqual(badRequest.Value, "Error");
        }

        [Test]
        public void UpdateGroup_UpdateExistGroup_ShouldReturnOk()
        {
            // Arrange 
            var group = new GroupUpdateRequest { Id = 1, Name = "FirstGroup" };
            managerGroup.Setup(m => m.UpdateGroup(It.IsAny<GroupUpdateRequest>())).Returns(new ResponseResult { Message = "Ok", IsSuccess = true });

            // Act 
            var result = controllerTest.UpdateGroup(group);

            // Assert 
            Assert.IsNotNull(result);
            managerGroup.Verify(m => m.UpdateGroup(It.IsAny<GroupUpdateRequest>()), Times.Once);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(okResult.Value, "Ok");
        }

        [Test]
        public void DeleteGroup_DeleteExistGroup_ShouldReturnOk()
        {
            // Arrange 
            var groupId = 1;
            managerGroup.Setup(m => m.DeleteGroup(It.IsAny<int>())).Returns(new ResponseResult { Message = "Ok", IsSuccess = true });

            // Act 
            var result = controllerTest.DeleteGroup(groupId);

            // Assert 
            Assert.IsNotNull(result);
            managerGroup.Verify(m => m.DeleteGroup(It.Is<int>(x => x == groupId)), Times.Once);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(okResult.Value, "Ok");
        }

        [Test]
        public void AddStudentToGroup_AddLink_ShouldReturnOk()
        {
            // Arrange 
            const int groupId = 1;
            const int studentId = 2;
            managerGroup.Setup(m => m.AddStudentToGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new ResponseResult {Message = "Ok", IsSuccess = true});

            // Act 
            var result = controllerTest.AddStudentToGroup(studentId, groupId);

            // Assert 
            Assert.IsNotNull(result);
            managerGroup.Verify(m => m.AddStudentToGroup(It.Is<int>(x => x == studentId), It.Is<int>(x => x == groupId)),
                Times.Once);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(okResult.Value, "Ok");
        }
        [Test]
        public void DeleteStudentFromGroup_DeleteLink_ShouldReturnOk()
        {
            // Arrange 
            const int groupId = 1;
            const int studentId = 2;
            managerGroup.Setup(m => m.DeleteStudentFromGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new ResponseResult {Message = "Ok", IsSuccess = true});

            // Act 
            var result = controllerTest.DeleteStudentFromGroup(studentId, groupId);

            // Assert 
            Assert.IsNotNull(result);
            managerGroup.Verify(m => m.DeleteStudentFromGroup(It.Is<int>(x => x == studentId), It.Is<int>(x => x == groupId)), Times.Once);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(okResult.Value, "Ok");
        }
    }
}
