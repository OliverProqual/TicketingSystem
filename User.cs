using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }      // maps to Users.username
        public string FullName { get; set; }      // maps to Users.full_name
        public string EmailAddress { get; set; }  // maps to Users.email

        public User() { }

        public User(int id, string userName, string fullName, string email)
        {
            UserID = id;
            UserName = userName;
            FullName = fullName;
            EmailAddress = email;
        }
    }
}
