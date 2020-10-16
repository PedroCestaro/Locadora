using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;

namespace Locadora.Domain
{
    public partial class TUser
    {
        public static Byte[] HashPassword(String password) 
        {
			return Service.HashPassword(password);
		}

        public virtual void Editar() 
        {
			Service.Editar(this);
		}

        public static TUser FindById(Int32 id) 
        {
			return Service.FindById(id);
		}

        public static TUser FindByEmail(String email) 
        {
			return Service.FindByEmail(email);
		}

        public static TUser FindByUsername(String username) 
        {
			return Service.FindByUsername(username);
		}

        public static TUser Authenticate(Login login) 
        {
			return Service.Authenticate(login);
		}

    }
}