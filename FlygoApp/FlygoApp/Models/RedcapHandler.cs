using System;
using FlygoApp.Commons;
using FlygoApp.Exceptions;
using FlygoApp.Persistency;
using FlygoApp.Views;

namespace FlygoApp.Models
{
    public class RedcapHandler
    {
        private readonly DtoFlyruteSingleton _dtoFlyrute;

        public SearchListSingleton SearchListSingleton;

        public NavigationService NavigationService;



        public RedcapHandler()
        {
            _dtoFlyrute = DtoFlyruteSingleton.GetInstance();
            SearchListSingleton = SearchListSingleton.GetInstance();
            NavigationService = new NavigationService();
        }

        public void SearchForFlyRute(string flyRuteNr, DateTime dateTime)
        {


            if (string.IsNullOrEmpty(flyRuteNr))
            {
                throw new NullOrEmptyException("Flyrute nummeret er tomt. Udfyld venligst dette!");
            }

            if (dateTime < DateTime.Now)
            {
                throw new DateWrongException("Datoen er mindre end dagsdato. Udfyld venligst korrekt dato!");
            }

            int x = _dtoFlyrute.FlyruteListe.Count;

            foreach (var rute in _dtoFlyrute.FlyruteListe)
            {
                x--;
                if (String.Equals(rute.FlyRuteNummer, flyRuteNr, StringComparison.CurrentCultureIgnoreCase) && rute.Afgang.Date == dateTime.Date)
                {
                    SearchListSingleton.FlyRute = rute;
                    NavigationService.Navigate(typeof(RedcapTaskListPage));
                    break;
                }

                if (x == 0)
                {
                    throw new InfoWrongException("Flyrute nummeret eller dato matcher ikke. Prøv venligst igen!");
                }

            }



        }


    }
}
