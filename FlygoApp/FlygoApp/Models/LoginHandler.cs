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
    public class LoginHandler
    {
        public DTOSingleton s;
        public NavigationService NavigationService;

        public LoginHandler()
        {
            s = DTOSingleton.GetInstance();
            NavigationService = new NavigationService();
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

                if (login.Key == brugernavn && login.Value.Password == kodeord)
                {
                    if (login.Value.RoleId == 1)
                    {
                        NavigationService.Navigate(typeof(HomePage));
                        break;
                    }
                    if (login.Value.RoleId == 2 || login.Value.RoleId == 3 || login.Value.RoleId == 4 ||
                        login.Value.RoleId == 5 || login.Value.RoleId == 6)
                    {
                        NavigationService.Navigate(typeof(WorkerPage));
                        break;
                    }
                    if (login.Value.RoleId == 7)
                    {
                        NavigationService.Navigate(typeof(RedcapTaskListPage));
                        break;
                    }
                    if (login.Value.RoleId == 8)
                    {
                        NavigationService.Navigate(typeof(HomePage));
                        break;
                    }

                    
                }

                if (x == 0)
                {
                    throw new LoginInfoWrongException("Brugernavnet eller kodeordet er forkert. Prøv venligst igen!");
                }
            }



        }

    }
}
