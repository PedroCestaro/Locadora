using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Locadora.Domain;

namespace Locadora.Web.Areas.Admin.ViewModel
{
    public class ReservationSearchViewModel
    {

        public string login { get; set; }
        public int movieId { get; set; }
        public bool returned { get; set; }
        public int? pagina { get; set; }

        public int usuario { get; set; }

        public int quantidade { get; set; }
          
        public ReservationSearchViewModel()
        {

        }

        public ReservationSearchViewModel(string login, int movieId, bool returned, int usuario)
        {
            this.login = login;
            this.movieId = movieId;
            this.returned = returned;
            this.usuario = usuario;
        }

        public ReservationSearchViewModel(string login)
        {
            this.login = login;
        }

        public ReservationSearchViewModel(int usuario)
        {
            this.usuario = usuario;
        }
        public ReservationSearch ConverToReservationSearch()
        {
            return new ReservationSearch()
            {
                login = this.login,
                movieId = this.movieId,
                returned = this.returned,
                page = this.pagina ?? 1,
                take = this.quantidade == 0 ? 10 : this.quantidade,
                usuario = this.usuario
            };
        }
    }
}

