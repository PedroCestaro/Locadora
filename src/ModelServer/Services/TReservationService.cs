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
using Remotion.Data.Linq;
using log4net.Core;
using NHibernate.Linq.Functions;
using NHibernate.SqlCommand;
using NHibernate.Util;
using NHibernate.Linq;
using NVelocity.Runtime.Parser.Node;
using System.Runtime.InteropServices;

namespace Locadora.Services
{
    public partial class TReservationService : EntityService<TReservation>, ITReservationService
    {
        public  ICriteria SearchCriteria(ReservationSearch search)
        {
            var criteria = Session.CreateCriteria<TReservation>();
         
            if (!string.IsNullOrWhiteSpace(search.login))
            {
                criteria.SetFetchMode("Client", FetchMode.Eager);//INNER JOIN DO CRITERIA
                criteria.CreateAlias("Client", "client", JoinType.LeftOuterJoin);
                criteria.Add(Restrictions.Eq("client.Name", search.login));
            }
             
            if (search.movieId.HasValue && search.movieId.Value !=0)
            {
                criteria.CreateAlias("TItens", "item", JoinType.LeftOuterJoin);
                criteria.SetFetchMode("item", FetchMode.Eager);           
                criteria.SetFetchMode("item.Movie", FetchMode.Eager);
                criteria.CreateAlias("item.Movie", "movie");
                criteria.Add(Restrictions.Eq("movie.Id", search.movieId));
            }

            if (search.returned.HasValue)
            {
                criteria.Add(Restrictions.Eq("Returned", search.returned));
            }

            //return criteria.List<TReservation>().ToList();
            return criteria;
        }
        
        public List<TReservation> Search(ReservationSearch search)
        {
            var criteria = SearchCriteria(search);
            criteria.SetFirstResult((search.page-1)*search.take).SetMaxResults(search.take);
            return criteria.List<TReservation>().ToList();
        }

        public int CountSearch(ReservationSearch search)
        {
            var criteria = SearchCriteria(search);
            return criteria.SetProjection(Projections.CountDistinct("Id")).UniqueResult<int>();
        }

    }

}
