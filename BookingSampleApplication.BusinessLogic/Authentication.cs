using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSampleApplication.DataAccess;

namespace BookingSampleApplication.BusinessLogic
{
    
    public class Authentication
    {
        AuthenticateUser dataAccessAuthenticate;

        public Authentication()
        {
            dataAccessAuthenticate = new AuthenticateUser();           
        }

        public bool Authenticate(string name, string password)
        {
            bool result = false;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@LoginName", name);
            parameters.Add("@Password", password);

            result = dataAccessAuthenticate.Autenticate(parameters);

            return result;
        }
    }
}
