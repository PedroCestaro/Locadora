using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Domain
{
    public class UserSecurity
    {
        public virtual Int32 Id { get; set; }
        public virtual String Username { get; set; }


        public UserSecurity(TClient client)
        {
            this.Id = client.Id;
            this.Username = client.Login;
        }

        public UserSecurity(TUser user)
        {
            this.Id = user.Id;
            this.Username = user.Name;
        }

    }
}
