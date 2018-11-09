using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
        }
    }
}