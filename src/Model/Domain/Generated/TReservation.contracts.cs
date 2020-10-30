using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using NHibernate;
using System.Collections.Generic;
using System;

namespace Locadora.Domain
{
    public partial class TReservation
    {
        public static ICriteria SearchCriteria(ReservationSearch search) 
        {
			return Service.SearchCriteria(search);
		}

        public static List<TReservation> Search(ReservationSearch search) 
        {
			return Service.Search(search);
		}

        public static Int32 CountSearch(ReservationSearch search) 
        {
			return Service.CountSearch(search);
		}

    }
}