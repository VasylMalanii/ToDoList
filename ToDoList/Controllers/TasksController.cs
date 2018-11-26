using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDoList.DTO;
using ToDoList.Models;
using ToDoList.DB.Repositories;

namespace ToDoList.Controllers
{
    public class TasksController : Controller
    {
        public IDbContext db;
        public CategoryRepository categoryRepository;
        public TaskRepository taskRepository;

        public TasksController()
        {
            categoryRepository = new CategoryRepository();
            taskRepository = new TaskRepository();
        }

        // GET: Tasks
        public ActionResult Tasks()
        {
            User account = (User)HttpContext.Session["account"];

            ViewBag.Title = "Tasks";
            if (account != null)
            {
                ViewBag.AccountId = account.Id;
                ViewBag.AccountName = account.Name;
            }

            return View();
        }

        [HttpGet]
        public ActionResult GetAccountCategories()
        {
            User currentUser = (User) HttpContext.Session["account"];
            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categoryDtos = categoryRepository.GetAll(currentUser.Id);
            return Json(categoryDtos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateCategory(CategoryDto categoryDto)
        {
            User currentUser = (User)HttpContext.Session["account"];
            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = categoryRepository.Add(categoryDto, currentUser.Id);
            JsonResult result = new JsonResult();
            result.Data = category;
            return result;
        }

        [HttpPost]
        public ActionResult CreateTask(TaskDto taskDto)
        {
            User currentUser = (User)HttpContext.Session["account"];
            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = taskRepository.Add(taskDto, currentUser.Id);
            JsonResult result = new JsonResult();
            result.Data = task;
            return result;
        }

        [HttpDelete]
        public ActionResult DeleteTask(int taskId)
        {
            User currentUser = (User)HttpContext.Session["account"];
            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var isTaskDeleted = taskRepository.Delete(taskId);
            if (isTaskDeleted == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPut]
        public ActionResult UpdateTask(TaskDto taskDto)
        {
            User currentUser = (User)HttpContext.Session["account"];
            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var isTaskUpdated = taskRepository.Update(taskDto);
            if (isTaskUpdated == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}