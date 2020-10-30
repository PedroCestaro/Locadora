using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Locadora.Domain;

namespace Locadora.Web.Areas.Admin.ViewModel
{
    public class CategorySearchViewModel
    {
        public int? pagina { get; set; }
        public int quantidade { get; set; }

        public string name { get; set; }
          
        public CategorySearchViewModel()
        {
      
        }


        public CategorySearch ConveryToCategorySearch()
        {
            return new CategorySearch()
            {
                name = this.name,
                page = this.pagina ?? 1,
                take = this.quantidade == 0 ? 10 : this.quantidade,
            };
        }

    }
}

