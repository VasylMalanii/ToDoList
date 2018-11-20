using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ToDoList.Helpers;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        public IDbContext db;

        public HomeController()
        {
            db = new DBModel();
        }

        public HomeController(IDbContext db)
        {
            this.db = db;
        }

        public ActionResult Index()
        {
            User account = (User)HttpContext.Session["account"];

            ViewBag.Title = "ExtremeTODO";
            if (account != null)
            {
                ViewBag.AccountId = account.Id;
                ViewBag.AccountName = account.Name;
            }

            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User user)
        {
            if (db.Users.Count(u => u.Email == user.Email) == 0)
            {
                user.Password = HashHelper.GetHash(user.Password);
                db.Users.Add(user);
                db.SaveChanges();

                AddUserToSession(user);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult LogIn(String email, String password)
        {
            var passwordHash = HashHelper.GetHash(password);
            User user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == passwordHash);
            if (user != null)
            {
                AddUserToSession(user);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            RemoveUserFromSession();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public int AddUserToSession(User user)
        {
            HttpContext.Session.Add("account", user);
            return 0;
        }

        public int RemoveUserFromSession()
        {
            HttpContext.Session.Remove("account");
            return 0;
        }
    }
}
