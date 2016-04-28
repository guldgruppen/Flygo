using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Models
{
    public class Flyrute
    {
        public DateTime Afgang { get; set; }
        public DateTime Ankomt { get; set; }
        public Fly Fly { get; set; }
        public string FlyruteNummer { get; set; }
        public Hangar Hangar { get; set; }
        public int Id { get; set; }


        public Flyrute(DateTime afgang, DateTime ankomt, Fly fly, string flyruteNummer, Hangar hangar, int id)
        {
            Afgang = afgang;
            Ankomt = ankomt;
            Fly = fly;
            FlyruteNummer = flyruteNummer;
            Hangar = hangar;
            Id = id;
        }

        public void CheckInfo()
        {
            
        }

    
    }
}
