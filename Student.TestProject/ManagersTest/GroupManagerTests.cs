using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Student.DataLayer;
using Student.DataLayer.Data;
using Student.DataLayer.Filters;
using Student.DataLayer.Repositories;
using Student.ServiceLayer.Core;
using Student.ServiceLayer.Models;

namespace Student.TestProject.ManagersTest
{
    [TestFixture]
    class GroupManagerTests
    {
        private GroupManager managerTest;
        private Mock<IGroupRepository> groupRepository;
        private Mock<IStudentRepository> studentRepository;

        [SetUp]
        public void Setup()
        {
            groupRepository = new Mock<IGroupRepository>();
            studentRepository = new Mock<IStudentRepository>();
            managerTest = new GroupManager(groupRepository.Object, studentRepository.Object);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetGroup_idRequest_shouldReturnGroupResponse(int id)
        {
            // Arrange
            int id1 = id + 1;
            int id2 = id - 1;
            const string groupName = "groupExpected";
            Group group1 = new Group(){Id = id1, Name = "group1"};
            Group group2 = new Group() {Id = id2, Name = "group2"};
            Group groupExpected = new Group() {Id = id, Name = groupName};
            
            groupRepository.Setup(g => g.Get(It.Is<int>(i => i == id1))).Returns(group1);
            groupRepository.Setup(g => g.Get(It.Is<int>(i => i == id2))).Returns(group2);
            groupRepository.Setup(g => g.Get(It.Is<int>(i => i == id))).Returns(groupExpected);

            // Act
            var result = managerTest.GetGroup(id);

            // Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, id);
            Assert.AreEqual(result.GroupName, groupExpected.Name);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void CreateGroup_AddNewGroup_ShouldReturnFalseResponseResult(string name)
        {
            // Arrange
            groupRepository.Setup(g => g.Create(It.Is<Group>(x => string.IsNullOrEmpty(x.Name))))
                .Callback(() => throw new Exception("Name group is null"));

            // Act 
            var result = managerTest.CreateGroup(name);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsSuccess, false);
            groupRepository.Verify(m => m.Create(It.Is<Group>(x => x.Name == name)), Times.Once);
        }

        [Test]
        [TestCase("group 1")]
        [TestCase("группа 1")]
        public void CreateGroup_AddNewGroup_ShouldReturnTrueResponseResult(string name)
        {
            // Arrange

            // Act 
            var result = managerTest.CreateGroup(name);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsSuccess, true);
            groupRepository.Verify(m => m.Create(It.Is<Group>(x => x.Name == name)), Times.Once);
            groupRepository.Verify(m => m.Create(It.IsAny<Group>()), Times.Once);
        }

        //[Test]
        ////Не удалось до конца решить проблему с IQueryable<T>
        ////[TestCase(FilterGroup.GroupName, "Группа", 2)]
        ////[TestCase(FilterGroup.GroupName, "Группа 1", 1)]
        //[TestCase(FilterGroup.None, null, 6)]
        //public async Task GetAllGroupsAsync_FilterRequest_shouldReturnGroups(FilterGroup filtredBy, string filterValue, int expectedCount)
        //{
        //    // Arrange
        //    GroupRequest request = new GroupRequest() { FiltredBy = filtredBy, FiltredValue = filterValue };
        //    var groups = getTestDataGroups().AsQueryable();
        //    var mockDbSet = new Mock<DbSet<Group>>();

        //    mockDbSet.As<IAsyncEnumerable<Group>>()
        //        .Setup(d => d.GetAsyncEnumerator(CancellationToken.None))
        //        .Returns(new AsyncEnumerator<Group>(groups.GetEnumerator()));

        //    mockDbSet.As<IQueryable<Group>>().Setup(m => m.Provider).Returns(groups.Provider);
        //    mockDbSet.As<IQueryable<Group>>().Setup(m => m.Expression).Returns(groups.Expression);
        //    mockDbSet.As<IQueryable<Group>>().Setup(m => m.ElementType).Returns(groups.ElementType);
        //    mockDbSet.As<IQueryable<Group>>().Setup(m => m.GetEnumerator()).Returns(groups.GetEnumerator());
        //    //mockDbSet.As<IQueryable<Group>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<TEnt>(data.Provider));

        //    //var studentGroups = getTestDataStudentGroup().AsQueryable();
        //    //var mockDbSetStudGroup = new Mock<DbSet<StudentGroup>>();

        //    //mockDbSetStudGroup.As<IAsyncEnumerable<StudentGroup>>()
        //    //    .Setup(d => d.GetAsyncEnumerator(CancellationToken.None))
        //    //    .Returns(new AsyncEnumerator<StudentGroup>(studentGroups.GetEnumerator()));

        //    //mockDbSetStudGroup.As<IQueryable<StudentGroup>>().Setup(m => m.Provider).Returns(studentGroups.Provider);
        //    //mockDbSetStudGroup.As<IQueryable<StudentGroup>>().Setup(m => m.Expression).Returns(studentGroups.Expression);
        //    //mockDbSetStudGroup.As<IQueryable<StudentGroup>>().Setup(m => m.ElementType).Returns(studentGroups.ElementType);
        //    //mockDbSetStudGroup.As<IQueryable<StudentGroup>>().Setup(m => m.GetEnumerator()).Returns(studentGroups.GetEnumerator());

        //    var mockCtx = new Mock<StudentDbContext>();
        //    mockCtx.SetupGet(c => c.Group).Returns(mockDbSet.Object);
        //    //mockCtx.SetupGet(c => c.StudentGroup).Returns(mockDbSetStudGroup.Object);
        //    groupRepository.Setup(g => g.GetQuery()).Returns(mockDbSet.Object);
        //    //groupRepository.Setup(g => g.GetQuery()).Returns(groups);

        //    // Act
        //    var result = await managerTest.GetAllGroupsAsync(request);
        //    //var result = await mockCtx.Object.Group.ToListAsync();

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.Count, expectedCount);
        //}

        //private List<Group> getTestDataGroups()
        //{
        //    var groups = new List<Group>()
        //    {
        //        new Group() {Id = 1, Name = "Группа 1"},
        //        new Group() {Id = 2, Name = "Группа 2"},
        //        new Group() {Id = 3, Name = "Group 1"},
        //        new Group() {Id = 4, Name = "Group 2"},
        //        new Group() {Id = 5, Name = "11"},
        //        new Group() {Id = 6, Name = "22"}
        //    };
        //    return groups;
        //}

        //private List<StudentGroup> getTestDataStudentGroup()
        //{
        //    int id = 0;
        //    var result = new List<StudentGroup>()
        //    {
        //        new StudentGroup() {Id = ++id, IdGroup = 1, IdStudent = 1},
        //        new StudentGroup() {Id = ++id, IdGroup = 1, IdStudent = 2},
        //        new StudentGroup() {Id = ++id, IdGroup = 1, IdStudent = 3},

        //        new StudentGroup() {Id = ++id, IdGroup = 2, IdStudent = 4},
        //        new StudentGroup() {Id = ++id, IdGroup = 2, IdStudent = 5},
        //        new StudentGroup() {Id = ++id, IdGroup = 2, IdStudent = 1},
        //    };
        //    return result;
        //}
    }
}
