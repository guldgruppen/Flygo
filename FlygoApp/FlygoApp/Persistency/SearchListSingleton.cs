using FlyGoWebService.Models;

namespace FlygoApp.Persistency
{
    //Indeholder flyopgaven der bliver søgt på, således at GC ikke fjerner den.
    public class SearchListSingleton
    {
        public Flyopgave Flyopgave;

        private static SearchListSingleton _instance;

        private SearchListSingleton()
        {
            Flyopgave = new Flyopgave();
        }

        public static SearchListSingleton GetInstance()
        {
            return _instance ?? (_instance = new SearchListSingleton());
        }

    }
}
