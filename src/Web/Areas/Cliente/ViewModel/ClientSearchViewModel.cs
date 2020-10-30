using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Locadora.Domain;

namespace Locadora.Web.Areas.Admin.ViewModel
{
    public class ClientSearchViewModel
    {

        public string name { get; set; }
        public int? pagina { get; set; }

        public int quantidade { get; set; }

        public ClientSearchViewModel()
        {

        }

        public ClientSearchViewModel(string name)
        {
            this.name = name;
        }

  
        public ClientSearch ConverToClientSearch()
        {
            return new ClientSearch()
            {
                name = this.name,
                page = this.pagina ?? 1,
                take = this.quantidade == 0 ? 10 : this.quantidade,
            };
        }
    }
}

