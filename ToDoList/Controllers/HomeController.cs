using System;
using System.Net;
using System.Web.Mvc;
using ToDoList.Models;
using ToDoList.DB.Repositories;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        UserRepository userRepository;

        public HomeController()
        {
            userRepository = new UserRepository();
        }

        // Method: GET. Request url: /home
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

        // Request url: /signup
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            var _user = userRepository.Add(user);
            if (_user != null)
            {
                AddUserToSession(user);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // Request url: /login
        [HttpPost]
        public ActionResult LogIn(String email, String password)
        {
            var user = userRepository.GetUserByCredentials(email, password);
            if (user != null)
            {
                AddUserToSession(user);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // Request url: /logout
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
