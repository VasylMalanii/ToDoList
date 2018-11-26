using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Models;

namespace ToDoList.DTO
{
    public class TaskDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int userId { get; set; }
        public int categoryId { get; set; }

        public TaskDto() { }

        public TaskDto(Task task)
        {
            this.id = task.Id;
            this.name = task.Name;
            this.description = task.Description;
            this.userId = task.UserId;
            this.categoryId = task.CategoryId;
        }

        public Task GetTask()
        {
            return new Task() {Id = id, Name = name, Description = description, CategoryId = categoryId, UserId = userId};
        }
    }
}