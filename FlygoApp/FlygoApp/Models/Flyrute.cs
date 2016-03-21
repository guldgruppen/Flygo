using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Models
{
    public class Flyrute
    {
        public string FlyruteNr { get; set; }
        public string Flytype { get; set; }
        public DateTime Ankomst { get; set; }
        public DateTime Afgang { get; set; }
        public string DestinationFra { get; set; }
        public string DestinationTil { get; set; }

        public Flyrute(string flyruteNr, string flytype, DateTime ankomst, DateTime afgang, string destinationFra, string destinationTil)
        {
            FlyruteNr = flyruteNr;
            Flytype = flytype;
            Ankomst = ankomst;
            Afgang = afgang;
            DestinationFra = destinationFra;
            DestinationTil = destinationTil;
        }

        public override string ToString()
        {
            return $"FlyruteNr: {FlyruteNr}, Flytype: {Flytype}, Ankomst: {Ankomst}, Afgang: {Afgang}, DestinationFra: {DestinationFra}, DestinationTil: {DestinationTil}";
        }
    }
}
