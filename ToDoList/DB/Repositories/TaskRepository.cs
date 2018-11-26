using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Models;
using ToDoList.DTO;
using ToDoList.Helpers;

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

        public TaskDto Add(TaskDto taskDto, int userId)
        {
            Task task = taskDto.GetTask();
            task.UserId = userId;
            db.Tasks.Add(task);
            db.SaveChanges();
            return new TaskDto(task);
        }
        public bool Update(TaskDto taskDto)
        {
            Task task = db.Tasks.FirstOrDefault(t => t.Id == taskDto.id);
            if (task == null) return false;
            task.Name = taskDto.name;
            task.Description = taskDto.description;
            db.SaveChanges();
            return true;
        }
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