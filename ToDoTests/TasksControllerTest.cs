using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ToDoList.Controllers;
using ToDoList.DTO;
using ToDoList.Helpers;
using ToDoList.Models;

namespace ToDoTests
{
    [TestClass]
    public class TasksControllerTest
    {

        private Mock<IDbContext> db;
        private TasksController tasksController;
        private User user;

        [TestInitialize]
        public void setup()
        {
            user = new User();
            user.Id = 1;

            var db = new Mock<IDbContext>();

            var controllerMock = new Mock<TasksController>(db.Object) { CallBase = true };
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.HttpContext.Session).Returns(session.Object);
            context.Setup(x => x.HttpContext.Session["account"]).Returns(user);
            tasksController = controllerMock.Object;
            tasksController.ControllerContext = context.Object;

            IDbSet<User> users = new FakeDbSet<User>()
            {
                new User(){Email = "email", Password = HashHelper.GetHash("password")}
            };
            IDbSet<Category> categories = new FakeDbSet<Category>()
            {
                new Category(){Id = 1, UserId = 1},
                new Category(){Id = 2, UserId = 1}
            };
            IDbSet<Task> tasks = new FakeDbSet<Task>()
            {
                new Task(){Id = 4, CategoryId = 1},
                new Task(){Id = 5, CategoryId = 1},
                new Task(){Id = 6, CategoryId = 2}
            };
            db.Setup(x => x.Users).Returns(users);
            db.Setup(x => x.Categories).Returns(categories);
            db.Setup(x => x.Tasks).Returns(tasks);
        }

        [TestMethod]
        public void TestGetAccountCategory()
        {
            JsonResult result = (JsonResult) tasksController.GetAccountCategories();

            List<CategoryDto> categories = (List<CategoryDto>) result.Data;
            Assert.AreEqual(2, categories.Count);
            Assert.AreEqual(2, categories[0].tasks.Count());
            Assert.AreEqual(1, categories[1].tasks.Count());
        }

        [TestMethod]
        public void TestCreateCategory()
        {
            CategoryDto categoryDto = new CategoryDto(){id = 10};

            JsonResult result = (JsonResult)tasksController.CreateCategory(categoryDto);

            CategoryDto category = (CategoryDto)result.Data;
            Assert.AreEqual(10, category.id);
        }

        [TestMethod]
        public void TestCreateTask()
        {
            TaskDto taskDto = new TaskDto() {id = 10};

            JsonResult result = (JsonResult)tasksController.CreateTask(taskDto);

            TaskDto task = (TaskDto)result.Data;
            Assert.AreEqual(10, task.id);
        }

        [TestMethod]
        public void TestDeleteTask()
        {
            HttpStatusCodeResult result = (HttpStatusCodeResult)tasksController.DeleteTask(4);

            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void TestDeleteTaskFail()
        {
            HttpStatusCodeResult result = (HttpStatusCodeResult)tasksController.DeleteTask(11111);

            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void TestUpdateTask()
        {
            TaskDto taskDto = new TaskDto(){id = 4, name = "name"};

            HttpStatusCodeResult result = (HttpStatusCodeResult)tasksController.UpdateTask(taskDto);

            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void TestUpdateTaskFail()
        {
            TaskDto taskDto = new TaskDto() { id = 232, name = "name" };

            HttpStatusCodeResult result = (HttpStatusCodeResult)tasksController.UpdateTask(taskDto);

            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
