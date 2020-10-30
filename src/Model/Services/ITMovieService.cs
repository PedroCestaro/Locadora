using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;
using NHibernate;
using System.Collections.Generic;

namespace Locadora.Services
{
    public partial interface ITMovieService : IEntityService<TMovie>, IService
    {
        void SaveCategories(TClient model);
        ICriteria SearchCriteria(MovieSearch movie);
        List<TMovie> Search(MovieSearch search);
        Int32 CountSearch(MovieSearch search);
    }
}