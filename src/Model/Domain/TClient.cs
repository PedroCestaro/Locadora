using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Locadora.Domain
{
    public partial class TClient
    {
        public virtual String PasswordString { get; set; }

        public virtual List<TCategory> Category { get; set; }//lista os meus objs categorias

        public virtual List<TPreference> Preference { get; set; }//quem tem preferencias é o cliente;

        public virtual int[] categories { get; set; }//array de ID de categorias

        public virtual int [] Reservas { get; set; }// quem possui reservas é o cliente

    }
}
