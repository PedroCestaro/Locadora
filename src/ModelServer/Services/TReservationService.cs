using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;
using NHibernate.Cache;
using NHibernate;
using NHibernate.Criterion;
using FluentNHibernate.Conventions;
using Locadora.Helpers;
using Simple;
using System.Security.Cryptography.X509Certificates;

namespace Locadora.Services
{
    public partial class TReservationService : EntityService<TReservation>, ITReservationService
    {

        //public List<TReservation> Search(ReservationSearch search)
        //{
        //    var criteria = SearchCriteria(search);
        //    criteria.AddOrder(Order.Desc("Date"));
        //    criteria.SetFirstResult((search.page - 1) * search.take).SetMaxResults(search.take);
        //    return criteria.List<TEvent>().DistinctBy(x => x.Id).ToList();
        //}


        public ICriteria SearchCriteria(ReservationSearch search)
        {
            var criteria = Session.CreateCriteria<TReservation>();

            if (!string.IsNullOrWhiteSpace(search.login))
                    criteria.Add(Restrictions.Eq("Client.Login", search.login));

            if (search.movieId.HasValue)
                criteria.Add(Restrictions.Eq("TIten.Movie.Id", search.movieId));

            return criteria;
           
        }
    }

}