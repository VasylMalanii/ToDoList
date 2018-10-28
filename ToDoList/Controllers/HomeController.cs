using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private DBModel db = new DBModel();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var user = new User();
            user.Id = 1;
            user.Name = "name";
            user.Email = "email";
            user.Password = "password";
            db.Users.Add(user);

            var category = new Category();
            category.Id = 1;
            category.Name = "name";
            category.UserId = user.Id;
            db.Categories.Add(category);

            var task = new Task();
            task.Id = 1;
            task.Name = "task";
            task.UserId = user.Id;
            task.CategoryId = category.Id;
            db.Tasks.Add(task);

            db.SaveChanges();

            return View();
        }
    }
}
