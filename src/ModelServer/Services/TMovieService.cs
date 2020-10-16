using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;

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

    }
}