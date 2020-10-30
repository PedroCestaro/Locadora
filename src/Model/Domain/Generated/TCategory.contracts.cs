using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using NHibernate;
using System.Collections.Generic;
using System;

namespace Locadora.Domain
{
    public partial class TCategory
    {
        public static ICriteria SearchCriteria(CategorySearch category) 
        {
			return Service.SearchCriteria(category);
		}

        public static List<TCategory> Search(CategorySearch search) 
        {
			return Service.Search(search);
		}

        public static Int32 CountSearch(CategorySearch search) 
        {
			return Service.CountSearch(search);
		}

    }
}