using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;
using NHibernate;
using NHibernate.Criterion;
using System.Security.Cryptography.X509Certificates;
using Simple;
using NHibernate.SqlCommand;

namespace Locadora.Services
{
    public partial class TMovieService : EntityService<TMovie>, ITMovieService
    {

        public void SaveCategories(TClient model)
        {
            TPreference.Delete(x => x.Client.Id == model.Id);
            for (int i = 0; i < model.categories.Length; i++)
            {
                new TPreference()
                {
                    Client = model,
                    Category = TCategory.Load(model.categories[i])
                }.Save();
            }
 
        }

        public ICriteria SearchCriteria(MovieSearch movie)
        {
            var criteria = Session.CreateCriteria<TMovie>();
            if (movie.active.HasValue)
                criteria.Add(Restrictions.Eq("IsActive", movie.active.Value));
            if (!movie.name.IsNullOrEmpty())
                criteria.Add(Restrictions.InsensitiveLike("Name", string.Format("%{0}%", movie.name.Trim())));
            if (movie.categories.HasValue)
            {
                criteria.CreateAlias("TMovieCategories", "tmoviecategory", JoinType.LeftOuterJoin);
                criteria.SetFetchMode("tmoviecategory", FetchMode.Eager);
                criteria.SetFetchMode("tmoviecategory.Category", FetchMode.Eager);
                criteria.CreateAlias("tmoviecategory.Category", "category");
                criteria.Add(Restrictions.Eq("category.Id", movie.categories));

            }
            return criteria;
        }

        public List<TMovie> Search(MovieSearch search)
        {
            var criteria = SearchCriteria(search);
            criteria.SetFirstResult((search.page - 1) * search.take).SetMaxResults(search.take);
            return criteria.List<TMovie>().ToList();
        }

        public int CountSearch(MovieSearch search)
        {
            var criteria = SearchCriteria(search);
            return criteria.SetProjection(Projections.CountDistinct("Id")).UniqueResult<int>();
        }


    }
}