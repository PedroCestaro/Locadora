using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System.Collections.Generic;

namespace Locadora.Services
{
    public partial interface ITReservationService : IEntityService<TReservation>, IService
    {
        List<TReservation> SearchCriteria(ReservationSearch search);
    }
}