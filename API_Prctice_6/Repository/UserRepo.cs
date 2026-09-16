using API_Prctice_6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Prctice_6.Repository
{
    public class UserRepo : IDisposable
    {
        private readonly OrderDBContext db = new OrderDBContext();

        public User ValidateUser(string username,string password)
        {
            var user =db.Users.FirstOrDefault(u=>u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password ==password);
            return user;
        }
        public void Dispose()
        {
           db.Dispose();
        }
    }
}