using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using NHibernate;

namespace Locadora.Services
{
    public partial interface ITReservationService : IEntityService<TReservation>, IService
    {
        ICriteria SearchCriteria(ReservationSearch search);
    }
}