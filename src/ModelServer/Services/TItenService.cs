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

namespace Locadora.Services
{
    public partial class TItenService : EntityService<TIten>, ITItenService
    {
        public void SaveItem (TReservation reservation)
        {
            var quantity = 0;
            var value = 0.0;
            TIten.Delete(x=>x.Reservation.Id == reservation.Id);
            for (int i = 0; i < reservation.Movies.Length;i++)
            {
               
                    var movie = TMovie.Load(reservation.Movies[i]);
                    value += 5.0;
                    quantity++;
                    new TIten()
                    {
                        Reservation = reservation,
                        Movie = movie,
                        Quantity = quantity,
                        Value = value
                    }.Save();
            }
               
        }




    }
}