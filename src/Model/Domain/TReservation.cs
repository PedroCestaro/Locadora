using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Domain
{
    public partial class TReservation
    {

        public virtual List<TMovie> Movie {get;set;}
        public virtual int [] Movies { get; set; }
        
        public virtual int [] Clientes { get; set; }

    }
}
