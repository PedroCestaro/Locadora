using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;
using NHibernate;
using NHibernate.Criterion;
using Simple;

namespace Locadora.Services
{
    public partial class TCategoryService : EntityService<TCategory>, ITCategoryService
    {
        public ICriteria SearchCriteria(CategorySearch category)
        {
            var criteria = Session.CreateCriteria<TCategory>();
            if (!category.name.IsNullOrEmpty())
                criteria.Add(Restrictions.Eq("Name", category.name));
            return criteria;
        }

        public List<TCategory> Search(CategorySearch search)
        {
            var criteria = SearchCriteria(search);
            criteria.SetFirstResult((search.page - 1) * search.take).SetMaxResults(search.take);
            return criteria.List<TCategory>().ToList();
        }

        public int CountSearch(CategorySearch search)
        {
            var criteria = SearchCriteria(search);
            return criteria.SetProjection(Projections.CountDistinct("Id")).UniqueResult<int>();
        }
    }
}