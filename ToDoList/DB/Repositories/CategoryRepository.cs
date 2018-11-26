using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Models;
using ToDoList.DTO;
using ToDoList.Helpers;

namespace ToDoList.DB.Repositories
{
    public class CategoryRepository
    {
        public IDbContext db;

        public CategoryRepository()
        {
            db = new DBModel();
        }
        public CategoryRepository(IDbContext db)
        {
            this.db = db;
        }

        public CategoryDto Add(CategoryDto categoryDto, int userId)
        {
            Category category = categoryDto.GetCategory();
            category.UserId = userId;
            db.Categories.Add(category);
            db.SaveChanges();
            return new CategoryDto(category, new List<TaskDto>());
        }

        public List<CategoryDto> GetAll(int userId)
        {
            List<CategoryDto> categoryDtos = new List<CategoryDto>();

            var categories = db.Categories.Where(c => c.UserId == userId).ToList();
            foreach (var category in categories)
            {
                var tasks = db.Tasks.Where(t => t.CategoryId == category.Id);
                IEnumerable<TaskDto> taskDtos = tasks.ToList().Select(t => new TaskDto(t));
                CategoryDto categoryDto = new CategoryDto(category, taskDtos);
                categoryDtos.Add(categoryDto);
            }
            return categoryDtos;
        }
    }
}