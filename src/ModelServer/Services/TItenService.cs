using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;
using System.Security.Cryptography.X509Certificates;
using NHibernate.Cache;
using NHibernate;
using NHibernate.Criterion;
using FluentNHibernate.Conventions;
using Simple.Validation;

namespace Locadora.Services
{
    public partial class TItenService : EntityService<TIten>, ITItenService
    {
        public void SaveItem (TReservation reservation)
        {
            int quantity;
            var value = 0.0;
            TIten.Delete(x=>x.Reservation.Id == reservation.Id);
           
                for (int i = 0; i < reservation.Movies.Length; i++)
                {

                    var movie = TMovie.Load(reservation.Movies[i]);
                    value += 05.00;
                    quantity = (int)reservation.Quantity[i];
                new TIten()
                {
                    Reservation = reservation,

                    Movie = movie,
                    Quantity = quantity,
                    Value = (decimal)value
                    }.Save();
                }
        }
 
    }
}