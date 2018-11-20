using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDoList.DTO;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TasksController : Controller
    {
        public IDbContext db;

        public TasksController()
        {
            db = new DBModel();
        }

        public TasksController(IDbContext db)
        {
            this.db = db;
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

            List<CategoryDto> categoryDtos = new List<CategoryDto>();

            var categories = db.Categories.Where(c => c.UserId == currentUser.Id).ToList();
            foreach (var category in categories)
            {
                var tasks = db.Tasks.Where(t => t.CategoryId == category.Id);
                IEnumerable<TaskDto> taskDtos = tasks.ToList().Select(t => new TaskDto(t));
                CategoryDto categoryDto = new CategoryDto(category, taskDtos);
                categoryDtos.Add(categoryDto);
            }

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

            Category category = categoryDto.GetCategory();
            category.UserId = currentUser.Id;
            db.Categories.Add(category);
            db.SaveChanges();
            JsonResult result = new JsonResult();
            result.Data = new CategoryDto(category, new List<TaskDto>());
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

            Task task = taskDto.GetTask();
            task.UserId = currentUser.Id;
            db.Tasks.Add(task);
            db.SaveChanges();
            JsonResult result = new JsonResult();
            result.Data = new TaskDto(task);
            return result;
        }

        [HttpDelete]
        public ActionResult DeleteTask(int taskId)
        {
            User currentUser = (User)HttpContext.Session["account"];
            Task task = db.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (currentUser == null || task == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            db.Tasks.Remove(task);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPut]
        public ActionResult UpdateTask(TaskDto taskDto)
        {
            User currentUser = (User)HttpContext.Session["account"];
            Task task = db.Tasks.FirstOrDefault(t => t.Id == taskDto.id);
            if (currentUser == null || task == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            task.Name = taskDto.name;
            task.Description = taskDto.description;
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}