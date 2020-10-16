using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using NHibernate;

namespace Locadora.Domain
{
    public partial class TReservation
    {
        public static ICriteria SearchCriteria(ReservationSearch search) 
        {
			return Service.SearchCriteria(search);
		}

    }
}