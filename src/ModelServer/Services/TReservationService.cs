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

namespace Locadora.Services
{
    public partial class TReservationService : EntityService<TReservation>, ITReservationService
    {
        public  List<TReservation> SearchCriteria(ReservationSearch search)
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

            return criteria.List<TReservation>().ToList();
        }
    }

}
