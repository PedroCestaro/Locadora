using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Domain
{
    public class MovieSearch
    {

        public int page { get; set; }
        public int take { get; set; }
        public string name { get; set; }
        public int? categories { get; set; }
        public bool?  active { get; set; }

        public MovieSearch()
        {

        }
    }
}
