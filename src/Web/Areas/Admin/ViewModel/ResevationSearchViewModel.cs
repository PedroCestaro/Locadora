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


        public ReservationSearchViewModel()
        {

        }

        public ReservationSearchViewModel(string login, int movieId)
        {
            this.login = login;
            this.movieId = movieId;
        }


        public ReservationSearch ConverToReservationSearch()
        {
            return new ReservationSearch()
            {
                login = this.login,
                movieId = this.movieId
            };
        }
    }
}

