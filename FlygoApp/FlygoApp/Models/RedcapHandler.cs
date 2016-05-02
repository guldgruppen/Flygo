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

        public void SearchForFlyRute(string flyRuteNr, DateTimeOffset dateTime)
        {
            SearchListSingleton.RedcapFlyRuteList.Clear(); 

            if (string.IsNullOrEmpty(flyRuteNr))
            {
                throw new NullOrEmptyException("Flyrute nummeret er tomt. Udfyld venligst dette!");
            }


            foreach (var rute in DtoSingleton.FlyruteListe)
            {
                
                if (rute.FlyRuteNummer == flyRuteNr && rute.Afgang == dateTime) 
                {
                    SearchListSingleton.RedcapFlyRuteList.Add(rute);
                    NavigationService.Navigate(typeof(RedcapTaskListPage));
                    break; 
                }
            }



        }


    }
}
