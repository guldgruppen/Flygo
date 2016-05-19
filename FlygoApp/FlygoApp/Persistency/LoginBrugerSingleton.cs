using FlyGoWebService;

namespace FlygoApp.Persistency
{
    public class LoginBrugerSingleton
    {
        private static LoginBrugerSingleton _loginBruger;
        public BrugerLogIn BrugerLogIn { get; set; }
        private LoginBrugerSingleton()
        {
            BrugerLogIn = new BrugerLogIn();
        }

        public static LoginBrugerSingleton GetInstance()
        {
            return _loginBruger ?? (_loginBruger = new LoginBrugerSingleton());
        }
    }
}
