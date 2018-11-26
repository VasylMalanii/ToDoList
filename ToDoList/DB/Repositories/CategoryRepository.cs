using System.Collections.Generic;
using System.Linq;
using ToDoList.Models;
using ToDoList.DTO;

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

        /// <summary>
        /// A Method to add a new Category
        /// </summary>
        /// <param name="categoryDto">Category object</param>
        /// <param name="userId">Id of a user Category belongs to</param>
        /// <returns>Added category with populated id field</returns>
        public CategoryDto Add(CategoryDto categoryDto, int userId)
        {
            if (categoryDto == null)
            {
                return null;
            }

            Category category = categoryDto.GetCategory();
            category.UserId = userId;
            db.Categories.Add(category);
            db.SaveChanges();
            return new CategoryDto(category, new List<TaskDto>());
        }

        /// <summary>
        /// Get list of all categories with tasks populated
        /// </summary>
        /// <param name="userId">Id of the current user</param>
        /// <returns>List of all categories for provided account</returns>
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