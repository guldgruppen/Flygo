using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Models
{
    public class Flyrute
    {
        public DateTimeOffset Afgang { get; set; }
        public DateTimeOffset Ankomt { get; set; }
        public Fly Fly { get; set; }
        public string FlyruteNummer { get; set; }
        public Hangar Hangar { get; set; }
        public int Id { get; set; }


        public Flyrute(DateTimeOffset afgang, DateTimeOffset ankomt, Fly fly, string flyruteNummer, Hangar hangar)
        {
            Afgang = afgang;
            Ankomt = ankomt;
            Fly = fly;
            FlyruteNummer = flyruteNummer;
            Hangar = hangar;
        }

        public void CheckInfo()
        {
            
        }

    
    }
}
