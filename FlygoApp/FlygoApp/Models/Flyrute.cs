using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Models
{
    public class Flyrute
    {
        private string _flyruteNr;
        private string _flytype;

        public string FlyruteNr
        {
            get { return _flyruteNr; }
            set
            {
                _flyruteNr = value;

            }
        }
        public string Flytype
        {
            get { return _flytype; }
            set
            {
                _flytype = value;

            }
        }
        public DateTime Ankomst { get; set; }
        public DateTime Afgang { get; set; }
        public string DestinationFra { get; set; }
        public string DestinationTil { get; set; }

        public Flyrute(string flyruteNr, string flytype, DateTime ankomst, DateTime afgang, string destinationFra, string destinationTil)
        {
            CheckFlyruteNr(flyruteNr);
            CheckFlyrutetype(flytype);
            CheckDestination(destinationFra);
            CheckDestination(destinationTil);
            //CheckDate(ankomst);
            //CheckDate(afgang);
            FlyruteNr = flyruteNr;
            Flytype = flytype;
            Ankomst = ankomst;
            Afgang = afgang;
            DestinationFra = destinationFra;
            DestinationTil = destinationTil;
        }

        public void CheckFlyruteNr(string flyrutenr)
        {
                if(String.IsNullOrEmpty(flyrutenr) || flyrutenr.Length > 6)
                throw new ArgumentException("Flyrute nummeret eksisterer ikke");
        }
        public void CheckFlyrutetype(string flytype)
        {
            if (String.IsNullOrEmpty(flytype))
                throw new ArgumentException("Flytypen eksisterer ikke");
        }
        public void CheckDestination(string destination)
        {
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentException("Destinationen eksisterer ikke");
        }
        public void CheckDate(DateTime dt)
        {
            if (dt < DateTime.Now) { }
            throw new ArgumentException("Afgang og ankomst skal forekomme i efter idag");
        }
        public override string ToString()
        {
            return $"FlyruteNr: {FlyruteNr}, Flytype: {Flytype}, Ankomst: {Ankomst}, Afgang: {Afgang}, DestinationFra: {DestinationFra}, DestinationTil: {DestinationTil}";
        }
    }
}
