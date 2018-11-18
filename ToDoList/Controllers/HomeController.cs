using System;
using System.Collections.Generic;
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
        private DBModel db = new DBModel();
        public ActionResult Index()
        {
            ViewBag.Title = "Extreme TODO";

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
                HttpCookie cookie = new HttpCookie("accountId");
                cookie.Value = user.Id.ToString();
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookie);
                HttpContext.Session.Add("account", user);
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
                HttpCookie cookie = new HttpCookie("accountId");
                cookie.Value = user.Id.ToString();
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookie);
                HttpContext.Session.Add("account", user);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            HttpContext.Session.Remove("account");
            if (Request.Cookies["accountId"] != null)
            {
                HttpCookie cookie = new HttpCookie("accountId");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
