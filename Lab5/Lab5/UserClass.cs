using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class UserClass
    {

        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public UserClass(string userName, string userEmail)
        {
            UserName = userName;
            UserEmail = userEmail;
        }
    }
}
