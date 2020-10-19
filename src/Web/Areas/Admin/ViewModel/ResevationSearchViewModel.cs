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
          
        public ReservationSearchViewModel()
        {

        }

        public ReservationSearchViewModel(string login, int movieId, bool returned)
        {
            this.login = login;
            this.movieId = movieId;
            this.returned = returned;
        }


        public ReservationSearch ConverToReservationSearch()
        {
            return new ReservationSearch()
            {
                login = this.login,
                movieId = this.movieId,
                returned = this.returned
            };
        }
    }
}

