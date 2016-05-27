using System;
using System.Linq;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;
using FlygoApp.Commons;
using FlygoApp.Exceptions;
using FlygoApp.Persistency;
using FlygoApp.Views;
using System.Threading.Tasks;

namespace FlygoApp.Models
{
    public class LoginHandler
    {
        private readonly DtoBrugerLoginSingleton _dtoBruger;
        public NavigationService NavigationService;
        private readonly DataMessengerSingleton _dataMessenger;

        public LoginHandler()
        {
            _dtoBruger = DtoBrugerLoginSingleton.GetInstance;
           _dataMessenger = DataMessengerSingleton.GetInstance;
            NavigationService = new NavigationService();
        }


        //Kontrollere om login info er korrekt
        public void CheckLoginInfo(string brugernavn, string kodeord)
        {
                CheckLoginException(brugernavn, kodeord);
                var result = _dtoBruger.BrugerLogInsDictionary.SingleOrDefault( bruger =>
                             bruger.Key.ToLower().Equals(brugernavn.ToLower()) &&
                             bruger.Value.Password.ToLower().Equals(kodeord.ToLower())
                             );

                if (result.Key == null || result.Value == null)
                {
                    throw new InfoWrongException("Brugernavnet eller kodeordet er forkert. Prøv venligst igen!");
                }
                _dataMessenger.BrugerLogIn.BrugerNavn = result.Key;
                _dataMessenger.BrugerLogIn.RoleId = result.Value.RoleId;

                    //Ser hvilken rolle brugeren har, og navigere derefter brugeren til deres respektive side
                    if (result.Value.RoleId == 1)
                    {                        
                        NavigationService.Navigate(typeof(HomePage));
                    }
                    if ((result.Value.RoleId >= 2 && result.Value.RoleId <= 7) || result.Value.RoleId == 9)
                    {
                        NavigationService.Navigate(typeof(WorkerTaskPage));
                    }
                    if (result.Value.RoleId == 8)
                    {
                        NavigationService.Navigate(typeof(AdminPage));
                    }

                    
         }

        private static void CheckLoginException(string brugernavn, string kodeord)
        {
            if (string.IsNullOrEmpty(brugernavn) && string.IsNullOrEmpty(kodeord))
            {
                throw new NullOrEmptyException("Brugernavnet og kodeordet er tomt. Venligst udfyld dette!");
            }
            else if (string.IsNullOrEmpty(brugernavn))
            {
                throw new NullOrEmptyException("Brugernavnet er tomt. Venligst udfyld dette!");
            }
            else if (string.IsNullOrEmpty(kodeord))
            {
                throw new NullOrEmptyException("Kodeordet er tomt. Venligst udfyld dette!");
            }
        }
    }
 }

   

