using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Locadora.Domain;

namespace Locadora.Web.Areas.Admin.ViewModel
{
    public class MovieSearchViewModel
    {
        public int? pagina { get; set; }

        public int quantidade { get; set; }

        public string nome { get; set; }

        public int? categorias { get; set; }

        public bool? ativo { get; set; }
          
        public MovieSearchViewModel()
        {
      
        }


        public MovieSearch ConveryToMovieSearch()
        {
            return new MovieSearch()
            {
                active = ativo,
                page = this.pagina ?? 1,
                take = this.quantidade == 0 ? 10 : this.quantidade,
                name = this.nome,
                categories = this.categorias
            };
        }

    }
}

