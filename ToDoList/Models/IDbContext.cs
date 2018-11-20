using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public interface IDbContext : IDisposable
    {
        IDbSet<User> Users { get; set; }
        IDbSet<Task> Tasks { get; set; }
        IDbSet<Category> Categories { get; set; }
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}