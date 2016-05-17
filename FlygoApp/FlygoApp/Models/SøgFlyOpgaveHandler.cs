using System;
using FlygoApp.Commons;
using FlygoApp.Exceptions;
using FlygoApp.Persistency;
using FlygoApp.Views;

namespace FlygoApp.Models
{
    public class SøgFlyOpgaveHandler
    {
        private readonly DtoFlyopgaveSingleton _dtoFlyopgave;

        public SearchListSingleton SearchListSingleton;

        public NavigationService NavigationService;



        public SøgFlyOpgaveHandler()
        {
            _dtoFlyopgave = DtoFlyopgaveSingleton.GetInstance();
            SearchListSingleton = SearchListSingleton.GetInstance();
            NavigationService = new NavigationService();
        }

        public void SearchForFlyopgave(string FlyopgaveNr, DateTime dateTime)
        {


            if (string.IsNullOrEmpty(FlyopgaveNr))
            {
                throw new NullOrEmptyException("Flyopgave nummeret er tomt. Udfyld venligst dette!");
            }

            if (dateTime < DateTime.Now.AddDays(-1))
            {
                throw new DateWrongException("Datoen er mindre end dagsdato. Udfyld venligst korrekt dato!");
            }

            int x = _dtoFlyopgave.FlyopgaveListe.Count;

            foreach (var rute in _dtoFlyopgave.FlyopgaveListe)
            {
                x--;
                if (String.Equals(rute.FlyopgaveNummer, FlyopgaveNr, StringComparison.CurrentCultureIgnoreCase) && rute.Afgang.Date == dateTime.Date)
                {
                    SearchListSingleton.Flyopgave = rute;
                    NavigationService.Navigate(typeof(RedcapTaskListPage));
                    break;
                }

                if (x == 0)
                {
                    throw new InfoWrongException("Flyopgave nummeret eller dato matcher ikke. Prøv venligst igen!");
                }

            }



        }


    }
}
