using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Models
{
    public class Flyrute
    {
        public int Id { get; set; }
        public DateTime Afgang { get; set; }
        public DateTime Ankomt { get; set; }
        public Destination DestinationFra { get; set; }
        public Destination DestinationTil { get; set; }
        public Fly Fly { get; set; }
        public string FlyruteNummer { get; set; }
        public Hangar Hangar { get; set; }

        public Flyrute(int id, DateTime afgang, DateTime ankomt, Destination destinationFra, Destination destinationTil, Fly fly, string flyruteNummer, Hangar hangar)
        {
            Id = id;
            Afgang = afgang;
            Ankomt = ankomt;
            DestinationFra = destinationFra;
            DestinationTil = destinationTil;
            Fly = fly;
            FlyruteNummer = flyruteNummer;
            Hangar = hangar;
        }


    
    }
}
