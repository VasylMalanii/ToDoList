using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ToDoList.Controllers;
using ToDoList.Helpers;
using ToDoList.Models;

namespace ToDoTests
{
    [TestClass]
    public class HomeControllerTest
    {

        private Mock<IDbContext> db;
        private HomeController homeController;

        [TestInitialize]
        public void setup()
        {
            var db = new Mock<IDbContext>();

            var controllerMock = new Mock<HomeController>(db.Object) {CallBase = true};
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.HttpContext.Session).Returns(session.Object);
            homeController = controllerMock.Object;
            homeController.ControllerContext = context.Object;

            IDbSet<User> users = new FakeDbSet<User>()
            {
                new User(){Email = "email", Password = HashHelper.GetHash("password")}
            };
            db.Setup(x => x.Users).Returns(users);
        }

        [TestMethod]
        public void TestSignup()
        {
            User user = new User() {Email = "newEmail", Password = "password"};

            HttpStatusCodeResult result = (HttpStatusCodeResult)homeController.SignUp(user);

            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void TestSignupFail()
        {
            User user = new User() { Email = "email", Password = "password" };

            HttpStatusCodeResult result = (HttpStatusCodeResult)homeController.SignUp(user);

            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void TestLogin()
        {
            HttpStatusCodeResult result = (HttpStatusCodeResult)homeController.LogIn("email", "password");

            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void TestLoginFailed()
        {
            HttpStatusCodeResult result = (HttpStatusCodeResult)homeController.LogIn("email1", "password1");

            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void TestLogout()
        {
    
            HttpStatusCodeResult result = (HttpStatusCodeResult)homeController.LogOut();

            Assert.AreEqual(200, result.StatusCode);
        }
    }
}
