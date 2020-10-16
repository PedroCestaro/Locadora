using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;
using System.Security.Cryptography.X509Certificates;
using NHibernate.Type;
using System.Xml.Linq;

namespace Locadora.Services
{
    public partial class TPreferenceService : EntityService<TPreference>, ITPreferenceService
    {
        public void SavePreferences(TClient model)//array no meu cliente
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