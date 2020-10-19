using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System.Collections.Generic;

namespace Locadora.Domain
{
    public partial class TReservation
    {
        public static List<TReservation> SearchCriteria(ReservationSearch search) 
        {
			return Service.SearchCriteria(search);
		}

    }
}