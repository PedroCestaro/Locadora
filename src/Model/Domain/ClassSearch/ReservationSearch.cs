﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Domain
{
    public class ReservationSearch
    {
        public String login { get; set; }
        public int? movieId { get; set; }
        public Boolean? returned { get; set; }
        public int usuario { get; set; }
        public int page { get; set; }
        public int take { get; set; }


        public ReservationSearch()
        {

        }
    }
}
