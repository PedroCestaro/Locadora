using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora
{
   public class AuthorizationException : Exception
    {
        public AuthorizationException() { }
        public AuthorizationException(string permission) : base(permission) { }
        public AuthorizationException(string permission, Exception inner) : base(permission, inner) { }

        protected AuthorizationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
