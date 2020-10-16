using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;

namespace Locadora.Services
{
    public partial interface ITMovieService : IEntityService<TMovie>, IService
    {
        void SaveCategories(TClient model);
    }
}