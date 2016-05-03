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
        private DTOSingleton s1; 
        public string FlyRuteNr { get; set; }

        public string Afgang { get; set; }
        public string Ankomst { get; set; }
        public int FlyId { get; set; }
        public Fly FlyType { get; set; }
        public int HangarId { get; set; }
        public Hangar Hangar { get; set; } 
        

         
        public RedcapTaskListViewModel()
        {
           
            s1 = DTOSingleton.GetInstance();
            s = SearchListSingleton.GetInstance();
            FlyRuteNr = s.FlyRute.FlyRuteNummer;
            Afgang = s.FlyRute.AfgangSomText;
            Ankomst = s.FlyRute.AnkomstSomText;
            FlyId = s.FlyRute.FlyId;
            HangarId = s.FlyRute.HangarId; 
            
            GetFlyObject();
            GetHangarObject();

        }

        public void GetFlyObject()
        {
            foreach (var fly in s1.FlyListe)
            {
                if (fly.Id == FlyId)
                {
                    FlyType = fly; 
                    break;
                }
            }          
        }

        public void GetHangarObject()
        {
            foreach (var hangar in s1.HangarListe)
            {
                if (hangar.Id == HangarId)
                {
                    Hangar = hangar; 
                    break;
                }
            }
        }


    }
}
