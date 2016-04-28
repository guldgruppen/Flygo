using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Commons;
using FlygoApp.Exceptions;
using FlygoApp.Persistency;
using FlygoApp.Views;

namespace FlygoApp.Models
{
    class LoginHandler
    {
        private DTOSingleton s;
        private NavigationService _navigationService; 

        public LoginHandler()
        {
            s = DTOSingleton.GetInstance();
            _navigationService = new NavigationService();
        }



        public void CheckLoginInfo(string brugernavn, string kodeord)
        {
            if (string.IsNullOrEmpty(brugernavn) && string.IsNullOrEmpty(kodeord))
            {
                throw new LoginIsNullOrEmptyException("Brugernavnet og kodeordet er tomt. Venligst udfyld dette!");
            }
            else if (string.IsNullOrEmpty(brugernavn))
            {
                throw new LoginIsNullOrEmptyException("Brugernavnet er tomt. Venligst udfyld dette!");
            }
            else if (string.IsNullOrEmpty(kodeord))
            {
                throw new LoginIsNullOrEmptyException("Kodeordet er tomt. Venligst udfyld dette!");
            }

            int x = s.BrugerLogInsDictionary.Count;

            foreach (var login in s.BrugerLogInsDictionary)
            { 
                x--; 

                if (login.Key == brugernavn && login.Value == kodeord)
                {
                    _navigationService.Navigate(typeof(HomePage));
                    break; 
                }

                if (x == 0)
                {
                    throw new LoginInfoWrongException("Brugernavnet eller kodeordet er forkert. Prøv venligst igen!");
                }
            }

            

        }

    }
}
