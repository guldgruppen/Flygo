using System;
using System.Linq;
using FlygoApp.Commons;
using FlygoApp.Exceptions;
using FlygoApp.Persistency;
using FlygoApp.Views;

namespace FlygoApp.Models
{
    public class SøgFlyOpgaveHandler
    {
        private readonly DtoFlyopgaveSingleton _dtoFlyopgave;

        private readonly DataMessengerSingleton _dataMessenger;

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

           var result = _dtoFlyopgave.FlyopgaveListe.SingleOrDefault(flyopgave => flyopgave.FlyopgaveNummer.ToLower().Equals(flyopgaveNr.ToLower()) && flyopgave.Afgang.Date.Equals(dateTime.Date));
            if (result == null)
            {
                throw new InfoWrongException("Flyopgave nummeret eller dato matcher ikke. Prøv venligst igen!");
            }
            _dataMessenger.Flyopgave = result;
            NavigationService.Navigate(typeof(WorkerTaskListPage));

        }
    }
}
