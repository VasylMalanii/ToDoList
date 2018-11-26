using System.Net;
using System.Web.Mvc;
using ToDoList.DTO;
using ToDoList.Models;
using ToDoList.DB.Repositories;

namespace ToDoList.Controllers
{
    public class TasksController : Controller
    {
        public IDbContext Db;
        public CategoryRepository CategoryRepository;
        public TaskRepository TaskRepository;

        public TasksController()
        {
            CategoryRepository = new CategoryRepository();
            TaskRepository = new TaskRepository();
        }

        // Method: GET. Request url: /
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

        // Request url: /get-account-categories
        [HttpGet]
        public ActionResult GetAccountCategories()
        {
            User currentUser = (User) HttpContext.Session["account"];
            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categoryDtos = CategoryRepository.GetAll(currentUser.Id);
            return Json(categoryDtos, JsonRequestBehavior.AllowGet);
        }

        // Request url: /post-category
        [HttpPost]
        public ActionResult CreateCategory(CategoryDto categoryDto)
        {
            User currentUser = (User)HttpContext.Session["account"];
            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = CategoryRepository.Add(categoryDto, currentUser.Id);
            JsonResult result = new JsonResult();
            result.Data = category;
            return result;
        }

        // Request url: /post-task
        [HttpPost]
        public ActionResult CreateTask(TaskDto taskDto)
        {
            User currentUser = (User)HttpContext.Session["account"];
            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = TaskRepository.Add(taskDto, currentUser.Id);
            JsonResult result = new JsonResult();
            result.Data = task;
            return result;
        }

        // Request url: /delete-task
        [HttpDelete]
        public ActionResult DeleteTask(int taskId)
        {
            User currentUser = (User)HttpContext.Session["account"];
            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var isTaskDeleted = TaskRepository.Delete(taskId);
            if (isTaskDeleted == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // Request url: /update-task
        [HttpPut]
        public ActionResult UpdateTask(TaskDto taskDto)
        {
            User currentUser = (User)HttpContext.Session["account"];
            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var isTaskUpdated = TaskRepository.Update(taskDto);
            if (isTaskUpdated == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}