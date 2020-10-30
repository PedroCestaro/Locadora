using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using NHibernate;
using System.Collections.Generic;
using System;

namespace Locadora.Services
{
    public partial interface ITReservationService : IEntityService<TReservation>, IService
    {
        ICriteria SearchCriteria(ReservationSearch search);
        List<TReservation> Search(ReservationSearch search);
        Int32 CountSearch(ReservationSearch search);
    }
}