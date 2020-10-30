using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;
using NHibernate;
using System.Collections.Generic;

namespace Locadora.Domain
{
    public partial class TMovie
    {
        public static void SaveCategories(TClient model) 
        {
			Service.SaveCategories(model);
		}

        public static ICriteria SearchCriteria(MovieSearch movie) 
        {
			return Service.SearchCriteria(movie);
		}

        public static List<TMovie> Search(MovieSearch search) 
        {
			return Service.Search(search);
		}

        public static Int32 CountSearch(MovieSearch search) 
        {
			return Service.CountSearch(search);
		}

    }
}