using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Exceptions;
using FlygoApp.Persistency;
using FlyGoWebService.Models;

namespace FlygoApp.Models
{
    class RedcapHandler
    {
        public DTOSingleton s;

        public List<FlyRute> RedcapFlyRuteList; 

        public RedcapHandler()
        {
            s = DTOSingleton.GetInstance();
            RedcapFlyRuteList = new List<FlyRute>();
        }

        public void SearchForFlyRute(string flyRuteNr, DateTime dateTime)
        {
            if (string.IsNullOrEmpty(flyRuteNr))
            {
                throw new NullOrEmptyException("Flyrute nummeret er tomt. Udfyld venligst dette!");
            }


            foreach (var rute in s.FlyruteListe)
            {
                if (rute.FlyRuteNummer == flyRuteNr && rute.Afgang == dateTime)
                {
                    RedcapFlyRuteList.Add(rute);
                }
            }


        }


    }
}
