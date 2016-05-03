using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlygoApp.Commons;
using FlygoApp.Models;
using FlygoApp.Persistency;
using FlygoApp.Views;

namespace FlygoApp.ViewModels
{
    public class RedcapTaskListViewModel
    {
        private SearchListSingleton s;
        private DTOSingleton s1;
        private ICommand _backCommand;
        private NavigationService navigationService;
        public string FlyRuteNr { get; set; }

        public string Afgang { get; set; }
        public string Ankomst { get; set; }
        public int FlyId { get; set; }
        public Fly FlyType { get; set; }
        public int HangarId { get; set; }
        public Hangar Hangar { get; set; }

        public ICommand BackCommand
        {
            get { return _backCommand ?? (new RelayCommand((() => navigationService.Navigate(typeof(RedcapTaskPage))))); }
            set { _backCommand = value; }
        }


        public RedcapTaskListViewModel()
        {

            //navigationService = new NavigationService();
            //s1 = DTOSingleton.GetInstance();
            //s = SearchListSingleton.GetInstance();
            //FlyRuteNr = s.FlyRute.FlyRuteNummer;
            //Afgang = s.FlyRute.AfgangSomText;
            //Ankomst = s.FlyRute.AnkomstSomText;
            //FlyId = s.FlyRute.FlyId;
            //HangarId = s.FlyRute.HangarId;
            //GetFlyObject();
            //GetHangarObject();


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
