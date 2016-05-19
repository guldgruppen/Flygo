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

        private DataMessengerSingleton _dataMessenger;

        public NavigationService NavigationService;



        public SøgFlyOpgaveHandler()
        {
            _dtoFlyopgave = DtoFlyopgaveSingleton.GetInstance;
            _dataMessenger = DataMessengerSingleton.GetInstance;
            NavigationService = new NavigationService();
        }

        //Søger på Brugerens indtastet data, og navigere dem til den detaljerede flyopgave liste.
        public void SearchForFlyopgave(string flyopgaveNr, DateTime dateTime)
        {


            if (string.IsNullOrEmpty(flyopgaveNr))
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
                if (String.Equals(rute.FlyopgaveNummer, flyopgaveNr, StringComparison.CurrentCultureIgnoreCase) && rute.Afgang.Date == dateTime.Date)
                {
                    _dataMessenger.Flyopgave = rute;
                    NavigationService.Navigate(typeof(WorkerTaskListPage));
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
