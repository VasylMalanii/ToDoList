using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TasksController : Controller
    {
        private DBModel db = new DBModel();

        // GET: Tasks
        public ActionResult Tasks()
        {
            User account = (User)HttpContext.Session["account"];

            ViewBag.Title = "Tasks";
            if (account != null)
            {
                ViewBag.AccountName = account.Name;
            }

            return View();
        }
    }
}