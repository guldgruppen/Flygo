using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Commons;
using FlygoApp.Exceptions;
using FlygoApp.Persistency;
using FlygoApp.Views;
using FlyGoWebService.Models;

namespace FlygoApp.Models
{
    public class RedcapHandler
    {
        public DTOSingleton DtoSingleton;

        public SearchListSingleton SearchListSingleton; 

        public NavigationService NavigationService; 



        public RedcapHandler()
        {
            DtoSingleton = DTOSingleton.GetInstance();
            SearchListSingleton = SearchListSingleton.GetInstance();
            NavigationService = new NavigationService();
        }

        public void SearchForFlyRute(string flyRuteNr, DateTime dateTime)
        {
            //SearchListSingleton.RedcapFlyRuteList.Clear();

            if (string.IsNullOrEmpty(flyRuteNr))
            {
                throw new NullOrEmptyException("Flyrute nummeret er tomt. Udfyld venligst dette!");
            }

            int x = DtoSingleton.FlyruteListe.Count; 

            foreach (var rute in DtoSingleton.FlyruteListe)
            {
                x--; 
                if (rute.FlyRuteNummer == flyRuteNr && rute.Afgang.Date == dateTime.Date)
                {
                    SearchListSingleton.FlyRute = rute; 
                    NavigationService.Navigate(typeof(RedcapTaskListPage));
                    break; 
                }

                if (x == 0)
                {
                    throw new InfoWrongException("Flyrute nummeret og dato matcher ikke. Prøv venligst igen!");
                }
                     
            }



        }


    }
}
