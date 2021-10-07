using System;
using System.Collections.Generic;
using System.Text;

namespace ItineraireApp.Models
{
    class ItineraireTypeResponse
    {
        public int idItineraire { get; set; }
        public int idVille { get; set; }
        public VilleTypeResponse idVilleNavigation { get; set; }
    }
}
