using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Domain
{
    public class Login
    {
        public string User { get; set; }
        public string Password { get; set; }

        public String ReturnUrl { get; set; }
        public Boolean Remember { get; set; }

        public Login(string user, string password)
        {
            this.User = user;
            this.Password = password;
        }  

        public Login() { }
    }
}
