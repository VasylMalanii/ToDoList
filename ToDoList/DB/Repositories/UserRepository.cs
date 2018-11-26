using System;
using System.Linq;
using ToDoList.Models;
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

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>User</returns>
        public User GetUserByEmail(string email)
        {
            return db.Users.FirstOrDefault(u => u.Email == email);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>Created user</returns>
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

        /// <summary>
        /// Get a user by email and password
        /// </summary>
        /// <param name="email">User Email</param>
        /// <param name="password">User Password</param>
        /// <returns>User by credentials</returns>
        public User GetUserByCredentials(String email, String password)
        {
            var passwordHash = HashHelper.GetHash(password);
            return db.Users.FirstOrDefault(u => u.Email == email && u.Password == passwordHash);
        }
    }
}