using System.Linq;
using ToDoList.Models;
using ToDoList.DTO;

namespace ToDoList.DB.Repositories
{
    public class TaskRepository
    {
        public IDbContext db;

        public TaskRepository()
        {
            db = new DBModel();
        }
        public TaskRepository(IDbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Add a new task
        /// </summary>
        /// <param name="taskDto">Task object</param>
        /// <param name="userId">Account Id</param>
        /// <returns>Task object with id populated</returns>
        public TaskDto Add(TaskDto taskDto, int userId)
        {
            Task task = taskDto.GetTask();
            task.UserId = userId;
            db.Tasks.Add(task);
            db.SaveChanges();
            return new TaskDto(task);
        }

        /// <summary>
        /// Update existing task
        /// </summary>
        /// <param name="taskDto">Task object</param>
        /// <returns>True if updated or False if unsuccessful</returns>
        public bool Update(TaskDto taskDto)
        {
            Task task = db.Tasks.FirstOrDefault(t => t.Id == taskDto.id);
            if (task == null) return false;
            task.Name = taskDto.name;
            task.Description = taskDto.description;
            db.SaveChanges();
            return true;
        }

        /// <summary>
        /// Removes a task
        /// </summary>
        /// <param name="taskId">Task Id to remove</param>
        /// <returns>True if deleted or False if unsuccessful</returns>
        public bool Delete(int taskId)
        {
            Task task = db.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null) return false;
            db.Tasks.Remove(task);
            db.SaveChanges();
            return true;
        }
    }
}