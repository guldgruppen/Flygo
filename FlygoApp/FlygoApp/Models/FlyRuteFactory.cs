using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyGoWebService.Models;

namespace FlygoApp.Models
{
    public class FlyRuteFactory : IFactory
    {
        
        public FlyRute CreateFlyrute(DateTimeOffset afgang, DateTimeOffset ankomst, int flyid, int hangarid, string nummer)
        {
            DateTime fra = DateTime.Parse(ankomst.ToString());
            DateTime til = DateTime.Parse(afgang.ToString());
          
            return new FlyRute() {Afgang = til, Ankomst = fra, FlyId = flyid,HangarId = hangarid, FlyRuteNummer = nummer};

        }

    }
}
