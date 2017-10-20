using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    class UserClass
    {
        public string UserName { get; set; }
        private string UserEmail { get; set; }
        private bool IsAdmin { get; set; }

        public UserClass(string userName, string userEmail, bool isAdmin = false)
        {
            this.UserName = userName;
            this.UserEmail = userEmail;
            this.IsAdmin = isAdmin;
        }
    }
}
