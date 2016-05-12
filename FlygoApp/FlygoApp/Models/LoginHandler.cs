using FlygoApp.Commons;
using FlygoApp.Exceptions;
using FlygoApp.Persistency;
using FlygoApp.Views;

namespace FlygoApp.Models
{
    public class LoginHandler
    {
        private readonly DtoBrugerLoginSingleton _dtoBruger;
        public NavigationService NavigationService;
        private readonly LoginBrugerSingleton _loginBruger;

        public LoginHandler()
        {
            _dtoBruger = DtoBrugerLoginSingleton.GetInstance();
            _loginBruger = LoginBrugerSingleton.GetInstance();
            NavigationService = new NavigationService();
        }



        public void CheckLoginInfo(string brugernavn, string kodeord)
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

            int x = _dtoBruger.BrugerLogInsDictionary.Count;

            foreach (var login in _dtoBruger.BrugerLogInsDictionary)
            {
                x--;
                _loginBruger.BrugerLogIn.BrugerNavn = login.Key;
                _loginBruger.BrugerLogIn.RoleId = login.Value.RoleId;
                
                if (login.Key == brugernavn && login.Value.Password == kodeord)
                {
                    if (login.Value.RoleId == 1)
                    {                        
                        NavigationService.Navigate(typeof(HomePage));
                        break;
                    }
                    if (login.Value.RoleId == 2 || login.Value.RoleId == 3 || login.Value.RoleId == 4 ||
                        login.Value.RoleId == 5 || login.Value.RoleId == 6 || login.Value.RoleId == 7 || login.Value.RoleId == 9)
                    {
                        NavigationService.Navigate(typeof(RedcapTaskPage));
                        break;
                    }
                    if (login.Value.RoleId == 8)
                    {
                        NavigationService.Navigate(typeof(AdminPage));
                        break;
                    }

                    
                }

                if (x == 0)
                {
                    throw new InfoWrongException("Brugernavnet eller kodeordet er forkert. Prøv venligst igen!");
                }
            }



        }

    }
}
