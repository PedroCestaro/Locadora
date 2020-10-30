using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using NHibernate;
using System.Collections.Generic;
using System;

namespace Locadora.Services
{
    public partial interface ITCategoryService : IEntityService<TCategory>, IService
    {
        ICriteria SearchCriteria(CategorySearch category);
        List<TCategory> Search(CategorySearch search);
        Int32 CountSearch(CategorySearch search);
    }
}