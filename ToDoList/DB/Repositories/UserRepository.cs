using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Models;
using ToDoList.DTO;
using ToDoList.Helpers;

namespace ToDoList.DB.Repositories
{
    public class UserRepository
    {
        public IDbContext db;

        public UserRepository()
        {
            db = new DBModel();
        }
        public UserRepository(IDbContext db)
        {
            this.db = db;
        }

        public User GetUserByEmail(string email)
        {
            return db.Users.FirstOrDefault(u => u.Email == email);
        }

        public User Add(User user)
        {
            if (GetUserByEmail(user.Email) != null)
            {
                user.Password = HashHelper.GetHash(user.Password);
                db.Users.Add(user);
                db.SaveChanges();
            }
            return GetUserByEmail(user.Email);
        }

        public User GetUserByCredentials(String email, String password)
        {
            var passwordHash = HashHelper.GetHash(password);
            return db.Users.FirstOrDefault(u => u.Email == email && u.Password == passwordHash);
        }
    }
}