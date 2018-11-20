using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Models;

namespace ToDoList.DTO
{
    public class CategoryDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int userId { get; set; }
        public IEnumerable<TaskDto> tasks { get; set; }

        public CategoryDto() { }

        public CategoryDto(Category category, IEnumerable<TaskDto> tasks)
        {
            this.id = category.Id;
            this.name = category.Name;
            this.description = category.Description;
            this.userId = category.UserId;
            this.tasks = tasks;
        }

        public Category GetCategory()
        {
            return new Category() {Id = id, Name = name, Description = description, UserId = userId};
        }
    }
}