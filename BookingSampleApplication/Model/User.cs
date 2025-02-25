using BookingSampleApplication.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSampleApplication
{
    public class User
    {
        Authentication authentication;
        public string Name { get; set; }
        public string Password { get; set; }

        public User()
        {
            authentication = new Authentication();
        }

        public bool Authnticate()
        {
            bool value = false;
            value = authentication.Authenticate(Name, Password);
            return value;

        }
    }
}
