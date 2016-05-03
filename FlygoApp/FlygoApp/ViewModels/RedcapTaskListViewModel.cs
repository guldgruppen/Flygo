using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Models;
using FlygoApp.Persistency;
using FlyGoWebService.Models;

namespace FlygoApp.ViewModels
{
    public class RedcapTaskListViewModel
    {
        private SearchListSingleton s;

        public string FlyRuteNr { get; set; }

        public string Afgang { get; set; }
        public string Ankomst { get; set; }
        public Fly FlyType { get; set; }
        public Hangar Hangar { get; set; }


        public RedcapTaskListViewModel()
        {  
            s = SearchListSingleton.GetInstance();
            FlyRuteNr = s.FlyRute.FlyRuteNummer;
            Afgang = s.FlyRute.AfgangSomText;
            Ankomst = s.FlyRute.AnkomstSomText;
            FlyType = s.FlyRute.Fly;
            Hangar = s.FlyRute.Hangar; 
        }

        


    }
}
