using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;

namespace Locadora.Domain
{
    public partial class TMovie
    {
        public static void SaveCategories(TClient model) 
        {
			Service.SaveCategories(model);
		}

    }
}