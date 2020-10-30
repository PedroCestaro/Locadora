using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;
using NHibernate;
using System.Collections.Generic;

namespace Locadora.Domain
{
    public partial class TClient
    {
        public static Byte[] HashPassword(String password) 
        {
			return Service.HashPassword(password);
		}

        public virtual void Edit() 
        {
			Service.Edit(this);
		}

        public static TClient FindById(Int32 id) 
        {
			return Service.FindById(id);
		}

        public static TClient FindByEmail(String email) 
        {
			return Service.FindByEmail(email);
		}

        public static TClient FindByUsername(String username) 
        {
			return Service.FindByUsername(username);
		}

        public static TClient Authenticate(Login login) 
        {
			return Service.Authenticate(login);
		}

        public static ICriteria SearchCriteria(ClientSearch search) 
        {
			return Service.SearchCriteria(search);
		}

        public static List<TReservation> Search(ClientSearch search) 
        {
			return Service.Search(search);
		}

        public static Int32 CountSearch(ClientSearch search) 
        {
			return Service.CountSearch(search);
		}

    }
}