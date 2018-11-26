using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Models;

namespace ToDoList.DTO
{
    public class UserDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public string expiredDate { get; set; }

        public UserDto() { }

        public UserDto(User user)
        {
            this.id = user.Id;
            this.name = user.Name;
            this.email = user.Email;
            this.password = user.Password;
            this.token = user.Token;
            this.expiredDate = user.ExpiredDate;
        }

        public User GetTask()
        {
            return new User() {Id = id, Name = name, Email = email, Password = password, Token = token, ExpiredDate = expiredDate};
        }
    }
}