using log4net.Util;
using Simple;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Domain
{
    public class SecurityContext
    {
        public static SecurityContext Do
        {
            get { return SimpleContext.Data.Singleton<SecurityContext>(); }
        }

        UserSecurity _user = null;
        bool _isAuthenticated = false;
        Func<IIdentity> _identityGetter = null;


        public void Demand(bool action)
        {
            if (!action) throw new AuthorizationException();
        }

        public bool IsAuthenticated { get { return _isAuthenticated; } }//instanciando o _isAuthenticated

        public UserSecurity User { get { return _user; } }//instanciando o _user

        

        public SecurityContext Init(Func<IIdentity> identityGetter)
        {
            _identityGetter = identityGetter;//recebendo a chave de identificação
            _isAuthenticated = TryGet(x => x.IsAuthenticated, false);//validando se está autenticado ou não

            var username = TryCast(x => x.Name, string.Empty);//chama a coleção declarada abaixo
            if (_isAuthenticated && !string.IsNullOrEmpty(username))//se _isAuthenticated e o username != null

            {
                    var model = TClient.FindByUsername(username);
                    if (model != null)
                        _user = new UserSecurity(model);
            }
            else if(_user == null)
            {
                var model = TUser.FindByUsername(username);
                if (model != null)
                    _user = new UserSecurity(model);
            }
            else
            {
                _user = null;
                _isAuthenticated = false;
            }
            return this;
        }

        public SecurityContext InitUser(Func<IIdentity> identityGetter)
        {
            _identityGetter = identityGetter;
            _isAuthenticated = TryGet(x => x.IsAuthenticated, false);

            var username = TryCast(x => x.Name, string.Empty);
            if (_isAuthenticated && !string.IsNullOrEmpty(username))
            {
                var client = TUser.FindByUsername(username);
                if (client != null)
                    _user = new UserSecurity(client);
                return this;
            }

            else
            {
                _user = null;
                if (_user == null)
                    _isAuthenticated = false;
                return this;
            }

        }

        public SecurityContext Init(SimpleContext context)
        {
            if (context != null && context.Username != null)
                Init(() => new GenericIdentity(context.Username));
            else
                Init(() => null);
            return this;
        }
        protected V TryCast<T, V>(Func<IIdentity, T> attr, V def)
                 where V : IConvertible
                 where T : class, IConvertible
            {
                var obj = TryGet<T>(attr, null);
                if (obj == null) return def;
                try
                {
                    return (V)Convert.ChangeType(obj, typeof(V));
                }
                catch (FormatException)
                {
                    return def;
                }
            }

           protected T TryGet<T>(Func<IIdentity, T> attr, T def)
            {
                try
                {
                    return attr(_identityGetter());
                }
                catch (NullReferenceException)
                {
                    return def;
                }
            }

       

    } 
}

