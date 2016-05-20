using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyGoWebService;
using FlyGoWebService.Models;

namespace FlygoApp.Models
{
    public class DataMessengerSingleton
    {
        private static DataMessengerSingleton _dataMessenger;

        public static DataMessengerSingleton GetInstance => _dataMessenger ?? (_dataMessenger = new DataMessengerSingleton());

        public Flyopgave Flyopgave { get; set; }
        public BrugerLogIn BrugerLogIn { get; set; }


        private DataMessengerSingleton()
        {
            Flyopgave = new Flyopgave();
            BrugerLogIn = new BrugerLogIn();
        }

        

    }
}
